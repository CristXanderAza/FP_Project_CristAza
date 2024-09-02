using Ruleta;
using System.IO;

public class GestoPerfiles{
 RegistroGeneral rg;

Dictionary<int, string> lRoles;

 List<Estudiante> lListadoDeEstudiantes;
 List<RegistroDeTirada> lListadoDeTiradas;

 string SendTo = "";


 string rutaDeLaCarpeta = @"C:\Users\pluto\OneDrive\Escritorio\projects\Ruleta\Perfiles";

   public GestoPerfiles(RegistroGeneral registroG){
     rg = registroG;
   }
   
   public void EscribirArchivo(string nombre){
        
        nombre = nombre + ".txt";
        string  rutaCompleta = Path.Combine(rutaDeLaCarpeta, nombre);

        try{
         using(StreamWriter swr = File.CreateText(rutaCompleta)){
           swr.WriteLine("#File:Start");
              swr.WriteLine("#ToList:Roles");
                List<string> roles = VaciarRoles();
                foreach(string s in roles){
                  swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
              swr.WriteLine("#ToList:Estudiantes");
                List<string> estudiantes = VaciarEstudiantes();
                foreach(string s in estudiantes){
                    swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
              swr.WriteLine("#ToList:Registros");
                List<string> rTiradas = VaciarRegistros();
                foreach(string s in rTiradas){
                    swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
           swr.WriteLine("#File:End");
         }
         rg.Mensaje = $"{rutaCompleta}, creada exitosamente";
 
        }
        catch(UnauthorizedAccessException){
            Console.WriteLine("No tienes permisos para crear el archivo.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("La carpeta especificada no existe.");
        }
    }

     List<string> VaciarRoles(){
        List<string> strings = new List<string>();
        for(int i = 0; i < rg.roles.Count; i++){
            string s = $"<({rg.CualRol(i)})>";
            strings.Add(s);
        }
       return strings;
     }
 
    List<string> VaciarEstudiantes(){
      List<string> strings = new List<string>();
      foreach(Estudiante estudiante in rg.ListadoDeEstudiantes){
        string rolesid = "";
        for(int i = 0; i < estudiante.rolesId.Count; i++){
          if(i == 0){
           rolesid += estudiante.rolesId[i];     
          }
          else{
            rolesid += $",{estudiante.rolesId[i]}"; 
          }
           
        }
        string s = $"<({estudiante.Nombre})({estudiante.Apellido})({estudiante.Matricula})({rolesid})>";
        strings.Add(s);
      }
      return strings;

    }

    List<string> VaciarRegistros(){
        List<string> strings = new List<string>();
        foreach(RegistroDeTirada rt in rg.ListadoDeTiradas){
            string rolesid ="";
            string estudiantesid = "";
            for(int i = 0; i < rt.listaDeRoles.Count; i++){
                if(i == 0){
                  rolesid+= $"{rt.listaDeRoles[i]}";
                }
                else{
                    rolesid += $",{rt.listaDeRoles[i]}";
                }
                
            }
            for(int i =0; i < rt.listaDeEstudiantes.Count; i++){
                 if(i == 0){
                  estudiantesid+= $"{rt.listaDeEstudiantes[i]}";
                }
                else{
                    estudiantesid += $",{rt.listaDeEstudiantes[i]}";
                }
            }
            string s = $"<({rt.date.ToString()})({rolesid})({estudiantesid})>";
            strings.Add(s);

        }
        return strings;
    }

    public string[] ObtenerNombresArchivos(){
      string[] strings = new string[3];
      try{
        string[] nombresCarpeta = Directory.GetFiles(rutaDeLaCarpeta);
        strings = nombresCarpeta;
      }
      catch (DirectoryNotFoundException)
        {
            Console.WriteLine("La carpeta especificada no existe.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No tienes permisos para acceder a la carpeta.");
        }
        return strings;
    }

    public void LeerArchivo(string rutaArchivo){
      
      try{
        using(StreamReader swr = new StreamReader(rutaArchivo)){
           string linea;
           while ((linea = swr.ReadLine()) != null)
           {
             InterPretarLinea(linea);
           }
        }
        rg.Mensaje = $"{rutaArchivo}, leido exitosamente";
      }
      catch (FileNotFoundException)
     {
      rg.Mensaje = $"El archivo no existe";
     
     }
     catch (UnauthorizedAccessException)
     {
      rg.Mensaje ="No tienes permisos para leer el archivo.";
     }

    }

   void InterPretarLinea(string linea){
      if(linea == "#File:Start"){
      lRoles = new Dictionary<int, string>();
      lListadoDeEstudiantes = new List<Estudiante>();
      lListadoDeTiradas = new List<RegistroDeTirada>();
    }
    else if(linea == "#File:End"){
      rg.ListadoDeEstudiantes = lListadoDeEstudiantes;
      rg.ListadoDeTiradas = lListadoDeTiradas;
      rg.roles = lRoles;
    }
   else if(linea == "#ToList:Roles"){
      SendTo = "roles";
    }
    else if(linea == "#ToList:Estudiantes"){
      SendTo = "estudiantes";
    }
    else if(linea == "#ToList:Registros"){
      SendTo = "rTiradas";
    }
    else if(linea == "#EndToList"){
      SendTo = "";
    }
    else if(linea.ToCharArray()[0] == '<'){
      List<string> parametros = new List<string>();
      string parametro = "";
      foreach(char c in linea){
        if(c == '('){
          parametro = "";
        }
        else if(c == ')'){
           parametros.Add(parametro);
        }
        else if(c == '>'){
           switch(SendTo){
            case "roles": AgregarRolL(parametros); break;
            case "estudiantes": AgregarEstudianteL(parametros); break;
            case "rTiradas" : AgregarRTiradaL(parametros); break;
           }
        }
        else{
          parametro += c;
        }
      }
    }

   }

   void AgregarRolL(List<string> parametros){
      foreach(string r in parametros){
        lRoles.Add(lRoles.Count, r);
      }
   }

   void AgregarEstudianteL(List<string> parametros){
      Estudiante estudiante = new Estudiante(parametros[0], parametros[1], parametros[2]);
      if(!string.IsNullOrEmpty(parametros[3])){
       string numeroAComvertir = "";
       for(int i = 0; i < parametros[3].Length; i++){
        if(parametros[3].ToCharArray()[i] == ','){
          estudiante.AgregarRol(Int32.Parse(numeroAComvertir));
          numeroAComvertir = "";
        }
        else if( i == parametros[3].Length -1){
          numeroAComvertir += parametros[3].ToCharArray()[i];
          estudiante.AgregarRol(Int32.Parse(numeroAComvertir));
        }
        else{
          numeroAComvertir += parametros[3].ToCharArray()[i];
        }
       } 
      }
      lListadoDeEstudiantes.Add(estudiante);
   }
   void AgregarRTiradaL(List<string> parametros){
      List<int> rolesIdt = new List<int>();
      List<int> estudiantesIdt = new List<int>();
      string numeroAComvertirR = "";
      for(int i = 0; i < parametros[1].Length; i++){
        if(parametros[1].ToCharArray()[i] == ','){
          rolesIdt.Add(Int32.Parse(numeroAComvertirR));
          numeroAComvertirR = "";
        }
        else if( i == parametros[1].Length -1){
          numeroAComvertirR += parametros[1].ToCharArray()[i];
          rolesIdt.Add(Int32.Parse(numeroAComvertirR));
        }
        else{
          numeroAComvertirR += parametros[1].ToCharArray()[i];
        }
       } 
       string numeroAComvertirE = "";
       for(int i =0; i < parametros[2].Length; i ++){
        if(parametros[2].ToCharArray()[i] == ','){
          estudiantesIdt.Add(Int32.Parse(numeroAComvertirE));
          numeroAComvertirE ="";
        }
        else if(i == parametros[2].Length -1){
          numeroAComvertirE += parametros[2].ToCharArray()[i];
          estudiantesIdt.Add(Int32.Parse(numeroAComvertirE));
        }
        else{
          numeroAComvertirE += parametros[2].ToCharArray()[i];
        }
       }
       lListadoDeTiradas.Add(new RegistroDeTirada(rolesIdt, estudiantesIdt, parametros[0]));
   }



   public void Sobreescribir(string rutaArchivo){
        try
        {
            // Crea o abre un archivo para escribir texto con codificación UTF-8
            using (StreamWriter swr = new StreamWriter(rutaArchivo, false)) // El segundo argumento es "append" (sobrescribir)
            {
                 swr.WriteLine("#File:Start");
              swr.WriteLine("#ToList:Roles");
                List<string> roles = VaciarRoles();
                foreach(string s in roles){
                  swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
              swr.WriteLine("#ToList:Estudiantes");
                List<string> estudiantes = VaciarEstudiantes();
                foreach(string s in estudiantes){
                    swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
              swr.WriteLine("#ToList:Registros");
                List<string> rTiradas = VaciarRegistros();
                foreach(string s in rTiradas){
                    swr.WriteLine(s);
                }
              swr.WriteLine("#EndToList");
           swr.WriteLine("#File:End");
            }

            rg.Mensaje = $"{rutaArchivo}, sobreescrito exitosamente";
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No tienes permisos para sobrescribir el archivo.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("La carpeta especificada no existe.");
        }

   }
}
using System.Xml.XPath;
using Ruleta;


public class Estudiante {
  
  public string Nombre {get; set;}
  public string Apellido {get; set;}
  public string Matricula {get; set;}
  public List<int> rolesId  = new List<int>();

  public Estudiante(string nombre, string apellido, string matricula = "20xx-xxxx"){

    Nombre = nombre;
    Apellido = apellido;  
    Matricula = matricula;

  }

  public void AgregarRol(int Id){

    rolesId.Add(Id);
  }

    public override string ToString()
    {
        return (Nombre + " " + Apellido);
    }


    public bool TengoEseRol(int rId){
 /*  int resultado; 
   resultado = rolesId.Find(n => n == rId);
   if(resultado != 0){
        return true;
    }
   else{
        return false;
    }
  */
  foreach(int rol in rolesId){
    if(rol == rId){
      return true;
    }
  }
  return false;

}
}






public class RegistroDeTirada{
 
   public List<int> listaDeRoles;

    public List<int> listaDeEstudiantes;
    public DateTime date;

    public RegistroDeTirada(List<int> roles, List<int> Estudiantes, string datel = null){

       listaDeRoles = roles;
       listaDeEstudiantes = Estudiantes;
       if(datel != null){
        DateTime.TryParse(datel, out date);
       }
       else{
        date = DateTime.Now;
      }
    }


}

public class RegistroGeneral{
   
   public List<Estudiante> ListadoDeEstudiantes = new List<Estudiante>();
   public List<RegistroDeTirada> ListadoDeTiradas = new List<RegistroDeTirada>();

   public string Mensaje;

  public  Dictionary<int, string> roles = new Dictionary<int, string>();

  public List<int> GanadoresEnTirada = new List<int>();

  public int RTiradaToView;

  public bool sVista;


  public RegistroGeneral(){
    /*
    roles.Add(0, "Programador en vivo");
    roles.Add(1, "Facilitador de contenido");
    
   

ListadoDeEstudiantes.Add(new Estudiante("Adrian", "Rojas Hidalgo"));
ListadoDeEstudiantes.Add(new Estudiante("Bladimir", "Díaz"));
ListadoDeEstudiantes.Add(new Estudiante("Bryam Leonardo", "Nuñez"));
ListadoDeEstudiantes.Add(new Estudiante("Cesar Jasser", "De Jesus Mejia"));
ListadoDeEstudiantes.Add(new Estudiante("Christopher", "Fernandez Valera"));
ListadoDeEstudiantes.Add(new Estudiante("Cristopher Xander", "Aza Diaz", "2023-0731"));
ListadoDeEstudiantes.Add(new Estudiante("Deury Dayron", "De La Cruz Ubiera"));
ListadoDeEstudiantes.Add(new Estudiante("Diana", "Guerrero Roble"));
ListadoDeEstudiantes.Add(new Estudiante("Eduardo Enmanuel", "Martinez Cespedes"));
ListadoDeEstudiantes.Add(new Estudiante("Elian Felix", "Leonardo"));
ListadoDeEstudiantes.Add(new Estudiante("Frankenny Ubrí", "Peréz"));
ListadoDeEstudiantes.Add(new Estudiante("Franklin Antonio", "Núñez Brito"));
ListadoDeEstudiantes.Add(new Estudiante("Gabriel", "Paulino"));
ListadoDeEstudiantes.Add(new Estudiante("Hilda", "Cedeño"));
ListadoDeEstudiantes.Add(new Estudiante("Jesús Manuel", "Heredia Jimenéz"));
ListadoDeEstudiantes.Add(new Estudiante("Jesus Steven", "Ramrez Felz"));
ListadoDeEstudiantes.Add(new Estudiante("Jimroy", "Atiles Sanchez"));
ListadoDeEstudiantes.Add(new Estudiante("Jose Alberto", "Dominguez Mejia"));
ListadoDeEstudiantes.Add(new Estudiante("Kerit Dimithar", "Machuca Mena"));
ListadoDeEstudiantes.Add(new Estudiante("Laura Rosmeris", "Vasquez Mateo"));
ListadoDeEstudiantes.Add(new Estudiante("Luis Angel", "Calderon Ramirez"));
ListadoDeEstudiantes.Add(new Estudiante("Maria Emilia", "Mancebo Ramirez"));
ListadoDeEstudiantes.Add(new Estudiante("Miguel Angel", "Cordero Sanchez"));
ListadoDeEstudiantes.Add(new Estudiante("Naomi", "Meran Padilla"));
ListadoDeEstudiantes.Add(new Estudiante("Orlando Jose", "Martinez Rodriguez"));
ListadoDeEstudiantes.Add(new Estudiante("Oscar Eduardo", "Cuello Sosa"));
ListadoDeEstudiantes.Add(new Estudiante("Rosemary Michelle", "Valdez"));
*/

  }

  public void AgregarRol(string NRol){
    roles.Add(roles.Count, NRol);
  }

  public void SetTiradaToView(int resta, bool solaVista){
    RTiradaToView = (ListadoDeTiradas.Count - resta);
    sVista = solaVista;
  }

  public string CualRol(int clave){
    
    

    if(roles.ContainsKey(clave)){
      
      return roles[clave];


    }
    return "";


  }

  public void AgregarUsuario(string nombre, string apellido, string matricula = "20xx-xxxx"){


    ListadoDeEstudiantes.Add(new Estudiante(nombre, apellido, matricula));
  }

  public void AgregarRegistro(RegistroDeTirada registroDeTirada){

    ListadoDeTiradas.Add(registroDeTirada);
  }

  public void NuevoMensaje(string s){
     Mensaje =s;
  }

  public List<int> EstudianteAptoParaEleccion(int rolAverificar){
      List<int> resultado = new List<int>();

      for(int i = 0; i < ListadoDeEstudiantes.Count; i++){
        if(!ListadoDeEstudiantes[i].TengoEseRol(rolAverificar)){
          if(NoHaGanado(i))
          resultado.Add(i);
        }
      }
     return resultado;



  }

  public bool NoHaGanado(int i){

    foreach(int a in GanadoresEnTirada){
       if(a == i){
        return false;
       }
       
    }
    return true;
  }


  






}

  public class Utilidades{

    public string ShortTo(string TexToShort, int s){

   
    string result = "";
   if(TexToShort.Length > 10){
    for  (int i = 0; i < s; i++){

      result += TexToShort.ToCharArray()[i]; 
    }}
    else{
      result = TexToShort;
    }

     return result;
     
      
    }
  }

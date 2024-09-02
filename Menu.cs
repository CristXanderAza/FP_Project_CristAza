using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Ruleta;
using System.Threading;
using System.Formats.Asn1;
using System.Globalization;

namespace Ruleta
{
    internal class Menu
    {
        public int AnchoVentana;
        public int LargoVentana;

        public ConsoleColor color;

        public Point LimiteSuperior;
        public Point LimiteInferior;
        
        public Menu(int ancho, int largo, ConsoleColor colors, Point limiteSuperior, Point limiteInferior){
           
           AnchoVentana = ancho;
           LargoVentana =  largo;
           color = colors;
           LimiteInferior = limiteInferior;
           LimiteSuperior = limiteSuperior;

          Reset();

        }

        void Reset(){
          Console.SetWindowSize(AnchoVentana, LargoVentana);
         Console.Title = "Ruleta";
         Console.BackgroundColor = color;
         Console.Clear();
         




        }

        public void DibujarUnMarco(List<string> menu, int indice){
                     
           for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = ConsoleColor.White;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = ConsoleColor.White;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         for(int i = 0; i < menu.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = ConsoleColor.White;
          if(i == indice ){
             textoAImprimir += " >";
            Console.ForegroundColor = ConsoleColor.Red;
          }
          else if(i == 0){
            textoAImprimir = "";
             Console.ForegroundColor = ConsoleColor.Yellow;
          }
          else{
            textoAImprimir = "  ";
          }
          textoAImprimir += menu[i];
          Console.WriteLine(textoAImprimir);

         }

         Console.WriteLine (indice);

        }
    }


    interface Imenu{

       bool Show(bool bActive);

       public bool itsActive {get; set;}
        void Input(ConsoleKeyInfo tecla);
        void Reset();
      }

    public class MenuPrincipal : Imenu {
         
      int index = 1;

      public bool itsActive {get; set;}

     Point LimiteSuperior = new Point (3,3);
     Point LimiteInferior =  new Point(28,16);
      
      public ConsoleColor  MyColor;

      public ConsoleColor SelectColor;

      public ConsoleColor colorTitulo;

      List<string> menu =  new List<string>(){"Menu", "Tirar", "Registro De tiradas", "Ajustes", "Perfiles", "Salir"};

      GestorMenus gestorM;

      
      public MenuPrincipal(GestorMenus gm){
       
       gestorM = gm;

      }
      public void Input(ConsoleKeyInfo tecla){


        if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <= 5 ){
                index -= 1;
            }
            else{
                index = 5;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index < 5 ){
                index += 1;
            }
            else{
                index = 1;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
      }

      public bool Show(bool bActive){
       itsActive = bActive;
       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         for(int i = 0; i < menu.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += " >";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = "  ";
          }
          textoAImprimir += menu[i];
          Console.WriteLine(textoAImprimir);

         }

         Console.WriteLine (index);
         return false;

        }

        void Output(){
          gestorM.SetMenu(1,index);
      
        }


      

      public void Reset(){
        index = 1;

        Console.SetWindowSize(170, 60);
         Console.Title = "Ruleta";
         Console.BackgroundColor = ConsoleColor.Black;
         Console.Clear();
      }


    }

    public class PhMenu : Imenu {
      public bool itsActive {get; set;}

      public bool Show(bool bActive){

        return true;
      }
        public void Input(ConsoleKeyInfo tecla){
         return;
        }
        public void Reset(){
            return;

        }

    }

    public class MenuSalida : Imenu {
         
      int index = 1;
     
     public bool itsActive {get; set;}

     Point LimiteSuperior = new Point (30,10);
     Point LimiteInferior =  new Point(44,16);
      
      public ConsoleColor  MyColor;

      public ConsoleColor SelectColor;

      public ConsoleColor colorTitulo;

      List<string> menu =  new List<string>(){"Estas Seguro?", "Si", "No",};

      GestorMenus gestorM;

      
      public MenuSalida(GestorMenus gm){
       
       gestorM = gm;

      }
      public void Input(ConsoleKeyInfo tecla){


        if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <= 2 ){
                index -= 1;
            }
            else{
                index = 2;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index < 2 ){
                index += 1;
            }
            else{
                index = 1;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorM.SetMenu(1,0);
        }
      }

      public bool Show(bool bActive){
       itsActive = bActive;

       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         for(int i = 0; i < menu.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += " >";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = "  ";
          }
          textoAImprimir += menu[i];
          Console.WriteLine(textoAImprimir);

         }

         Console.WriteLine (index);
         return false;

        }

        void Output(){
          if(index == 1){
            Console.Clear();
            gestorM.setExit();
          }
          if(index == 2) {
            Reset();
            Console.Clear();
            gestorM.SetMenu(1,0);
          }
      
        }


      

      public void Reset(){
        index = 1;

      }


    }


    public class menuVotacion : Imenu {
      public bool itsActive {get; set;}

       

       public RegistroGeneral registroGeneral;

       public List<Votacion> votaciones = new List<Votacion>();

       GestorMenus GestMenus;

       Votacion votacionRef;

       public bool itsFirstTime;

      
      public menuVotacion(RegistroGeneral r, GestorMenus gestor){

        registroGeneral = r;
        GestMenus = gestor;


      }
    
      public bool Show(bool Active){

       itsActive = Active; 
       GestMenus.itsListening = false;
       if(!itsFirstTime){
        votacionRef = new Votacion(0,0, registroGeneral,new Point(1,1), new Point (2,2), GestMenus);
         vaciarVotaciones();

       }
       itsFirstTime = true;
     if(itsActive){  
      if(GestMenus.votacionViable){
     for(int i=0; i <5; i ++){
       if(votaciones.Count < 9){
       foreach(Votacion votacion in votaciones){
        votacion.show();
       }
       }
       else{
         for(int a = 0; a < 8; a++){
           votaciones[a].show();
         }
         RenderExceso(votaciones.Count -8, votaciones[6].LimiteInferior);
       }
       
       Thread.Sleep(1000);
       
     }
     AgregarRoles();
     GuardarRegistro();
     foreach(Votacion V in votaciones){
      V.WriteIndex = 0;
     }
     registroGeneral.GanadoresEnTirada.Clear();}
    
     else{
        RenderAler();
     }}
      GestMenus.itsListening = true;
       GestMenus.SetMenu(2,1);
         return false;
      }

      void RenderExceso(int exceso, Point LimiteSREF){
         Point LimiteSuperior = new Point(LimiteSREF.X - 12, LimiteSREF.Y + 1);
         Point LimiteInferior = new Point(LimiteSREF.X + 14, LimiteSREF.Y + 3 );

          ConsoleColor MyColor = ConsoleColor.White;


          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + 1 );
           Console.ForegroundColor = ConsoleColor.Red;
           Console.Write($"{exceso} más");
         
      }

      
      void RenderAler(){
           Point LimiteSuperior = new Point(30,5);

          Point LimiteInferior = new Point(100,7);
          ConsoleColor MyColor = ConsoleColor.White;


          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + 1 );
           Console.ForegroundColor = ConsoleColor.Red;
           Console.Write("Cantidad de Estudiantes insuficientes para todos los roles");
           Thread.Sleep(5000);
           Console.Clear();
           Reset();
           GestMenus.GeneralReset();


      }
      void AgregarRoles(){
        foreach(Votacion votacion in votaciones){
          registroGeneral.ListadoDeEstudiantes[votacion.ganador].AgregarRol(votacion.miRol);
        }
      }

      void GuardarRegistro(){
        List<int> rolesDeLosGanadores = new List<int>();
        List<int> Ganadores = new List<int>();

        foreach(Votacion v in  votaciones){
          rolesDeLosGanadores.Add(v.miRol);
          Ganadores.Add(v.ganador);
        }

        registroGeneral.AgregarRegistro(new RegistroDeTirada(rolesDeLosGanadores, Ganadores));
        registroGeneral.SetTiradaToView(1, true);
      }

      void vaciarVotaciones(){
         for(int i = 0; i < registroGeneral.roles.Count; i++ ){
          Votacion v =new Votacion(i,i,registroGeneral, votacionRef.LimiteSuperior, votacionRef.LimiteInferior, GestMenus);
          votaciones.Add(v);
          if(i % 2 == 0 ){
            votacionRef = v;
          }
         }
      }

     
        public void Input(ConsoleKeyInfo tecla){
          if(tecla.Key == ConsoleKey.LeftArrow){
             Console.Clear();
             Reset();
            GestMenus.SetMenu(1,0);
        }
        else if(tecla.Key == ConsoleKey.RightArrow){
           GestMenus.SetMenu(2,1);
        }
        
        
       
        }
        public void Reset(){
           itsFirstTime = false;
           votaciones = new List<Votacion>();

        }


    }

    public class Votacion{
    public  Point LimiteSuperior = new Point (30,4);
    public  Point LimiteInferior =  new Point(44,10);
      
      public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

      public List<int> estudiantes;

      public int miRol;

      GestorMenus gestorM;
      public int miOrden;

      public int ganador; 

      public int WriteIndex;

      

      public RegistroGeneral registro;      
      public Votacion(int rol, int orden, RegistroGeneral r, Point puntoSuperiorDeReferencia, Point PuntoInferiorDeReferencia, GestorMenus gm){
       registro = r;
       gestorM = gm;
       miRol = rol;
       miOrden = orden;
       Seleccionar();
       AjustarVentana(puntoSuperiorDeReferencia, PuntoInferiorDeReferencia);
       

        
      }

      void Seleccionar(){
        List<int> Aptos = registro.EstudianteAptoParaEleccion(miRol);
        List<int> Ganadores = new List<int>();
      if(Aptos.Count > 0){
        for(int i = 0; i < 5; i++){
          Random aleatorio = new Random();
          int seleccionado = aleatorio.Next(0, (Aptos.Count));
          Ganadores.Add(Aptos[seleccionado]);

        }
        estudiantes = Ganadores;
        ganador = Ganadores[4];
        registro.GanadoresEnTirada.Add(ganador);
        }
        else{
          gestorM.votacionViable = false;
        }
      } 

      void AjustarVentana(Point Psr, Point Pir){
       if(miOrden == 0){
        LimiteSuperior = new Point (30, 3);
        LimiteInferior = new Point (42, 10);
       }
       else if(miOrden % 2 == 0){
        LimiteSuperior = new Point(Psr.X, (Pir.Y +1));
        LimiteInferior = new Point(Pir.X, (Pir.Y + 8));
       }
       else{
        LimiteSuperior = new Point ((Pir.X + 2), Psr.Y);
        LimiteInferior = new Point((Pir.X + 14), Pir.Y);
       }
       
      }

      public void show(){
       
          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Resultados

            
         for(int i = 0; i <= (WriteIndex + 1) ; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i ==(WriteIndex + 1) ){
             textoAImprimir += "";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = gestorM.utilidades.ShortTo(registro.CualRol(miRol), 10);
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0)
          textoAImprimir += gestorM.utilidades.ShortTo(registro.ListadoDeEstudiantes[estudiantes[i-1]].ToString(), 10);
          Console.WriteLine(textoAImprimir);

         }
         WriteIndex ++;
         
         
         

        }
        

      }

      public class MenuRegistroUno : Imenu {
        RegistroGeneral registroGeneral1;

        RegistroDeTirada miRegistroDeTirada;
        GestorMenus gestorMenus1;

      public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

        Point LimiteSuperior = new Point (58,3);

        int desplazamientoMenu = 0;
        int index =0;

        Point LimiteSuperiorEscritura = new Point(58,8);
        Point LimiteInferior =  new Point(170,16);
        public MenuRegistroUno(GestorMenus gm, RegistroGeneral rg){
          registroGeneral1 = rg;
          gestorMenus1 = gm;
          
          
        }
       
        public bool itsActive {get; set;}
        public bool Show(bool bActive)
        {
          itsActive = bActive;
          
          miRegistroDeTirada = registroGeneral1.ListadoDeTiradas[(registroGeneral1.RTiradaToView)];
          if(! (miRegistroDeTirada.listaDeEstudiantes.Count <= 8))
          Console.Clear();
          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");
          
           Console.SetCursorPosition(LimiteSuperior.X + 56 -(miRegistroDeTirada.date.ToString().Length/2), LimiteSuperior.Y + 1 );
           Console.ForegroundColor = ConsoleColor.Yellow;
           Console.Write(miRegistroDeTirada.date.ToString());
           Console.ForegroundColor = ConsoleColor.White;
           MakeFrontier();
           MakeColums();
          if(miRegistroDeTirada.listaDeEstudiantes.Count <= 8){
           for(int i = 0; i < miRegistroDeTirada.listaDeEstudiantes.Count; i++){
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 1, LimiteSuperiorEscritura.Y + i);
            Console.Write(miRegistroDeTirada.listaDeEstudiantes[i]);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 11, LimiteSuperiorEscritura.Y + i);
            Console.Write(registroGeneral1.CualRol(i));
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 51, LimiteSuperiorEscritura.Y + i);
            Console.Write($"{registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Nombre} {registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Apellido}  ");
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 91, LimiteSuperiorEscritura.Y + i);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Matricula);
            
           }
           }
          else{
          for(int i = 0 + desplazamientoMenu; i < 8 + desplazamientoMenu; i++){
           
           if(index == i){
            Console.ForegroundColor = ConsoleColor.Red;
           }
           
           
            
           
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 1, LimiteSuperiorEscritura.Y + i - desplazamientoMenu);
           Console.Write(miRegistroDeTirada.listaDeEstudiantes[i]);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 11, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
             Console.Write(registroGeneral1.CualRol(i));
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 51, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write($"{registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Nombre} {registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Apellido}  ");
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 91, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[miRegistroDeTirada.listaDeEstudiantes[i]].Matricula);
            
            Console.ForegroundColor = ConsoleColor.White;
           }
         }
         

         return false;
        }

        void MakeFrontier(){
           for(int i = LimiteSuperior.X + 1 ; i <= LimiteInferior.X - 1; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y + 2);
              Console.Write("═");                       
           }
           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y +2);
           Console.Write("╠");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y +2);
           Console.Write("╣");

           
        }

        void MakeColums(){
           for(int i = LimiteSuperior.Y + 2; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X + 10 , i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteSuperior.X + 50 , i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteSuperior.X + 90, i);
              Console.Write("║");
           }
               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteSuperior.Y + 2);
              Console.Write("╦");
              Console.SetCursorPosition(LimiteSuperior.X + 50 , LimiteSuperior.Y + 2);
              Console.Write("╦");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteSuperior.Y + 2);
              Console.Write("╦");

               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteInferior.Y );
              Console.Write("╩");
              Console.SetCursorPosition(LimiteSuperior.X + 50 ,LimiteInferior.Y );
              Console.Write("╩");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteInferior.Y );
              Console.Write("╩");
              
              Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + 3);
              Console.WriteLine("ID");
              Console.SetCursorPosition(LimiteSuperior.X + 11, LimiteSuperior.Y + 3);
              Console.WriteLine("Posicion");
              Console.SetCursorPosition(LimiteSuperior.X + 51, LimiteSuperior.Y + 3);
              Console.WriteLine("Estudiante");
              Console.SetCursorPosition(LimiteSuperior.X + 91, LimiteSuperior.Y + 3);
              Console.WriteLine("Matricula");

               for(int i = LimiteSuperior.X + 1 ; i <= LimiteInferior.X - 1; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y + 4);
              Console.Write("═");
             
              


           }
           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y +4);
           Console.Write("╠");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y +4);
           Console.Write("╣");

              
               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteSuperior.Y + 4 );
              Console.Write("╬");
              Console.SetCursorPosition(LimiteSuperior.X + 50 ,LimiteSuperior.Y + 4  );
              Console.Write("╬");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteSuperior.Y + 4  );
              Console.Write("╬");

              

              




        }

        
       
        public void Input(ConsoleKeyInfo tecla){
            if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 0 && index <= miRegistroDeTirada.listaDeEstudiantes.Count -1){
                index -= 1;
                if(index == 0 + desplazamientoMenu && desplazamientoMenu != 0){
                  desplazamientoMenu --;
                }
            }
            else{
                /*index = miRegistroDeTirada.listaDeEstudiantes.Count -1;
                desplazamientoMenu = miRegistroDeTirada.listaDeEstudiantes.Count- 9;*/
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 0 && index < miRegistroDeTirada.listaDeEstudiantes.Count -1 ){
                if(index == 7 + desplazamientoMenu && index < miRegistroDeTirada.listaDeEstudiantes.Count -1){
                  desplazamientoMenu ++;
                }
                index += 1;
            }
            else{
                index = 0;
                desplazamientoMenu = 0;
            }
        } 
         else if(tecla.Key == ConsoleKey.LeftArrow){
            Console.Clear();
            if(registroGeneral1.sVista){
            gestorMenus1.GeneralReset();}
            else{
              Console.Clear();
              gestorMenus1.SetMenu(2,0);
            }
          }

        }
        public void Reset(){

        }


      }

      public class MenuRegistrosView : Imenu {
          public bool itsActive {get; set;}

          Point LimiteSuperior = new Point(30,5);

          Point LimiteInferior = new Point(56,16);

          GestorMenus gestorMenus;
          RegistroGeneral registroGeneral;

          public ConsoleColor  MyColor;

          public ConsoleColor SelectColor;

          public ConsoleColor colorTitulo;

          public int index = 1;

          int desplazamientoMenu = 0;


          public MenuRegistrosView(GestorMenus gm, RegistroGeneral rg){
           gestorMenus = gm;
           registroGeneral = rg;

          }
       public bool Show(bool bActive){
         itsActive = bActive;

         if(!(registroGeneral.ListadoDeTiradas.Count <= 9) && itsActive){
          Console.Clear();
         }

       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         if(registroGeneral.ListadoDeTiradas.Count <= 9){
         for(int i = 0; i <= registroGeneral.ListadoDeTiradas.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "Selecciona la tirada";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0){
           textoAImprimir += registroGeneral.ListadoDeTiradas[registroGeneral.ListadoDeTiradas.Count -i].date;
          }
          
          Console.WriteLine(textoAImprimir);

         }
         }
         else{
          for( int i = 0 + desplazamientoMenu; i < 10 + desplazamientoMenu; i++){
            Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1) - desplazamientoMenu);
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0 + desplazamientoMenu){
            textoAImprimir = "Selecciona la tirada";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0 + desplazamientoMenu){
           textoAImprimir += registroGeneral.ListadoDeTiradas[registroGeneral.ListadoDeTiradas.Count -i].date;
          }
          
          Console.WriteLine(textoAImprimir);
          }
         }
         Console.Write(index);

        return false;
       }

       
        public void Input(ConsoleKeyInfo tecla){ 
          if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <=  registroGeneral.ListadoDeTiradas.Count ){
                index -= 1;
                 if(index == 0 + desplazamientoMenu && desplazamientoMenu != 0){
                  desplazamientoMenu --;
                }
            }
            else{
                index =  registroGeneral.ListadoDeTiradas.Count;
                desplazamientoMenu = registroGeneral.ListadoDeTiradas.Count - 9;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index <  registroGeneral.ListadoDeTiradas.Count ){
                  if(index == 9 + desplazamientoMenu && index < registroGeneral.ListadoDeTiradas.Count ){
                  desplazamientoMenu ++;
                }
                index += 1;
            }
            else{
                index = 1;
                desplazamientoMenu = 0;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){
          if(registroGeneral.ListadoDeTiradas.Count != 0)
          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorMenus.SetMenu(1,0);
        }
        }

        void Output(){
         registroGeneral.SetTiradaToView(index,false);
         gestorMenus.SetMenu(2,1);
        }
        public void Reset(){
           
        }



      }

      public class MenuAjustes : Imenu {
         int index = 1;
     
     public bool itsActive {get; set;}

     Point LimiteSuperior = new Point (30,10);
     Point LimiteInferior =  new Point(56,16);
      
      public ConsoleColor  MyColor;

      public ConsoleColor SelectColor;

      public ConsoleColor colorTitulo;

      List<string> menu =  new List<string>(){"Ajustes", "Añadir Estudiante", "Añadir Rol", "Ver Estudiantes"};

      GestorMenus gestorM;

      
      public MenuAjustes(GestorMenus gm){
       
       gestorM = gm;

      }
      public void Input(ConsoleKeyInfo tecla){


        if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <= menu.Count -1){
                index -= 1;
            }
            else{
                index = menu.Count -1;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index < menu.Count -1 ){
                index += 1;
            }
            else{
                index = 1;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorM.SetMenu(1,0);
        }
      }

      public bool Show(bool bActive){
       itsActive = bActive;
       
       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         for(int i = 0; i < menu.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += " >";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = "  ";
          }
          textoAImprimir += menu[i];
          Console.WriteLine(textoAImprimir);

         }

         Console.WriteLine (index);
         return false;

        }

        void Output(){
          if(index == 1){
            Console.Clear();
            gestorM.SetMenu(2,2);
          }
          else if(index == 2) {
            Console.Clear();
            gestorM.SetMenu(2,3);
          }
          else if(index ==3){
            Console.Clear();
            gestorM.SetMenu(2,6);
          }
          else{
            /*Reset();
            Console.Clear();
            gestorM.SetMenu(1,0); */
          }
      
        }


      

      public void Reset(){
        index = 1;

      }



      }

      public class MenuNuevoUsuario : Imenu {
       public bool itsActive {get; set;}
       public GestorMenus gm;
       public RegistroGeneral rg;

       public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

       Point LimiteSuperior = new Point (58,10);

       Point LimiteSuperiorEscritura = new Point (58,11);

       
        Point LimiteInferior =  new Point(170,16);

        public int index = 0;

        string[,] Tex= new string[3,2];

        

       public MenuNuevoUsuario(GestorMenus g, RegistroGeneral r){
        gm = g;
        rg = r;
        Tex[0,0] = "Nombre";
        Tex[1,0] = "Apellido";
        Tex[2,0] = "Matricula";

        Tex[0,1] = "";
        Tex[1,1] = "";
        Tex[2,1] = "";
       }

        public bool Show(bool bActive){
          itsActive = bActive;
           for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");
          CrearDivision();
         return false;
        }

        public void CrearDivision(){
          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
            if(i == LimiteSuperior.X){
              Console.SetCursorPosition(i, LimiteSuperior.Y + 2);
            Console.Write("╠");
            Console.SetCursorPosition(i, LimiteSuperior.Y + 4);
            Console.Write("╠");
            }
            else if(i == LimiteInferior.X){
              Console.SetCursorPosition(i, LimiteSuperior.Y + 2);
            Console.Write("╣");
            Console.SetCursorPosition(i, LimiteSuperior.Y + 4);
            Console.Write("╣");
            }
            else{
            Console.SetCursorPosition(i, LimiteSuperior.Y + 2);
            Console.Write("═");
            Console.SetCursorPosition(i, LimiteSuperior.Y + 4);
            Console.Write("═");
            }
          }
          for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.SetCursorPosition(LimiteSuperior.X + 20, i);
              Console.Write("║");
          }
          Console.SetCursorPosition(LimiteSuperior.X + 20, LimiteSuperior.Y + 2);
          Console.Write("╬");
          Console.SetCursorPosition(LimiteSuperior.X + 20, LimiteSuperior.Y + 4);
          Console.Write("╬");

          Console.SetCursorPosition(LimiteSuperior.X + 20, LimiteSuperior.Y);
          Console.Write("╦");
          Console.SetCursorPosition(LimiteSuperior.X + 20, LimiteInferior.Y );
          Console.Write("╩");

          for(int i = 0; i < 5; i+=2 ){
            if(i/2 == index){
              Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 1, LimiteSuperiorEscritura.Y + i);
            Console.Write(Tex[i/2,0]);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 21, LimiteSuperiorEscritura.Y + i);
            Console.Write(Tex[i/2,1]);
            Console.ForegroundColor = MyColor;
          }
        }

        public void Input(ConsoleKeyInfo tecla){
           
        if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 0 && index <= 2){
                index -= 1;
            }
            else{
                index = 2;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 0 && index < 2 ){
                index += 1;
            }
            else{
                index = 0;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){
          if(index ==0 || index == 1){
            index +=1;
          }
          else{
            Output();
          }
          
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gm.SetMenu(2,0);
            Reset();
        }
        else if(char.IsLetter(tecla.KeyChar) && (index == 0 || index == 1)){
          if(Tex[index,1].Length < 21)
          Tex[index,1] += tecla.KeyChar;

        }
        else if(char.IsDigit(tecla.KeyChar) &&  index ==2){
          if(Tex[index,1].Length < 8)
          Tex[index,1] += tecla.KeyChar;
        }
        else if(tecla.Key == ConsoleKey.Backspace){
          if(!string.IsNullOrEmpty(Tex[index,1])){
            Tex[index,1] = Tex[index,1].Substring(0, Tex[index,1].Length -1);
            Console.Clear();
          }
        }
        else if(tecla.Key == ConsoleKey.Spacebar){
          if(index != 2){
            Tex[index,1] += " ";
          }
        }
        }
        void Output(){
          bool ex = existenciaDeEstudiante();
         if(!string.IsNullOrEmpty(Tex[0,1]) && !string.IsNullOrEmpty(Tex[1,1]) && !ex){
         if(Tex[2,1].Length == 8){
          rg.AgregarUsuario(Tex[0,1],Tex[1,1],Tex[2,1]);
         }
         else if(string.IsNullOrEmpty(Tex[2,1]) || Tex[2,1].Length <= 2){
          rg.AgregarUsuario(Tex[0,1],Tex[1,1]);
         }}
         else if(ex){
          Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y  + 2);
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("Ya hay un estudiante con este nombre");
          Thread.Sleep(1000);
         }
         else{
          Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y  + 2);
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("Debes llenar correctamente los campos. La matricula debe tener 8 digitos");
          Thread.Sleep(1000);
         }
         Console.Clear();
         gm.GeneralReset();
        }

        bool existenciaDeEstudiante(){
          bool resultado = false;
          foreach(Estudiante es in rg.ListadoDeEstudiantes){
            if(es.Nombre == Tex[0,1]){
              if(es.Apellido == Tex[1,1])
              resultado = true;
              return resultado;
            }
          }
          return resultado;
        }
        public void Reset(){
          Tex[0,1] = "";
        Tex[1,1] = "";
        Tex[2,1] = "";
        index = 0;
        }



      }

      public class MenuNuevoRol: Imenu{
        public bool itsActive {get; set;}

          public GestorMenus gm;
       public RegistroGeneral rg;

       public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

        Point LimiteSuperior = new Point (58,10);      
        Point LimiteInferior =  new Point(170,12);

        string RolNuevo = "";

         public MenuNuevoRol(GestorMenus g, RegistroGeneral r){
          gm = g;
          rg = r;
         }
         
         public bool Show(bool bActive){
            itsActive = bActive;
              for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");
           CrearDivision();
           Console.ForegroundColor = SelectColor;
           Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y +1);
           Console.Write("Nuevo Rol");
           Console.SetCursorPosition(LimiteSuperior.X + 16, LimiteSuperior.Y +1);
           Console.Write(RolNuevo);
           Console.ForegroundColor = MyColor;
           return false;
         }

          public void CrearDivision(){
          for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
             
             Console.SetCursorPosition(LimiteSuperior.X + 15, i);
              Console.Write("║");
          }
           Console.SetCursorPosition(LimiteSuperior.X + 15, LimiteSuperior.Y );
                Console.Write("╦");
            
                Console.SetCursorPosition(LimiteSuperior.X + 15, LimiteInferior.Y );
                Console.Write("╩");
         
          }

       
        public void Input(ConsoleKeyInfo tecla){
         if(tecla.Key == ConsoleKey.RightArrow){
           if(!string.IsNullOrEmpty(RolNuevo)){
           if(!confirmarExistencia()){
            rg.AgregarRol(RolNuevo);
            Reset();
            gm.GeneralReset();
           }
           else{
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y  + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("El Rol ya existe");
            Thread.Sleep(1000);
            Console.Clear();
           }
           }
           else{
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y  + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Debes rellenar todos los campos");
            Thread.Sleep(1000);
            Console.Clear();
           }
         }
         else if(tecla.Key== ConsoleKey.LeftArrow){
          gm.SetMenu(2,0);
          Reset();
          Console.Clear();
         }
       
        else if(char.IsLetter(tecla.KeyChar)){
          if(RolNuevo.Length < 21)
          RolNuevo += tecla.KeyChar;

        }
        else if(tecla.Key == ConsoleKey.Backspace){
          if(!string.IsNullOrEmpty(RolNuevo)){
            RolNuevo = RolNuevo.Substring(0, RolNuevo.Length -1);
            Console.Clear();
          }
        }
        else if(tecla.Key == ConsoleKey.Spacebar){
          
            RolNuevo += " ";
          
        }
        }

        bool confirmarExistencia(){
          
          for(int i = 0; i < rg.roles.Count; i++){
            if(RolNuevo == rg.CualRol(i)){
              return true;
            }
          }
          return false;
        }
        public void Reset(){
          RolNuevo = "";
        }
      }

      public class MenuPerfiles : Imenu{
        int index = 1;
     
     public bool itsActive {get; set;}

     Point LimiteSuperior = new Point (30,10);
     Point LimiteInferior =  new Point(56,16);
      
      public ConsoleColor  MyColor;

      public ConsoleColor SelectColor;

      public ConsoleColor colorTitulo;

      List<string> menu =  new List<string>(){"Perfiles","Escribir", "Leer", "Sobreescribir"};

      GestorMenus gestorM;

      RegistroGeneral rg;

      
      public MenuPerfiles(GestorMenus gm, RegistroGeneral registroGeneral1){
       
       gestorM = gm;
       rg = registroGeneral1;
      }
      public void Input(ConsoleKeyInfo tecla){


        if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <= menu.Count -1){
                index -= 1;
            }
            else{
                index = menu.Count -1;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index < menu.Count -1 ){
                index += 1;
            }
            else{
                index = 1;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorM.SetMenu(1,0);
            Reset();
        }
      }

      public bool Show(bool bActive){
       itsActive = bActive;

       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         for(int i = 0; i < menu.Count; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += " >";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = "  ";
          }
          textoAImprimir += menu[i];
          Console.WriteLine(textoAImprimir);

         }

         Console.WriteLine (index);
         return false;

        }

        void Output(){
          /*if(index == 1){
            Console.Clear();
            gestorM.SetMenu(2,2);
          }
          if(index == 2) {
            Console.Clear();
            gestorM.SetMenu(2,3);
          }
          else{
            Reset();
            Console.Clear();
            gestorM.SetMenu(1,0);
          }*/

          switch(index){
           case 1: 
              Console.Clear();
              gestorM.SetMenu(2,4);
              break;
           case 2: 
              Console.Clear();
              gestorM.SetMenu(2,5);
              break;
            case 3: 
            Console.Clear();
              gestorM.SetMenu(2,7);
              break;

          }
      
        }


      

      public void Reset(){
        index = 1;

      }
      }

      public class MenuNuevoPerfil: Imenu{
        public bool itsActive {get; set;}

          public GestorMenus gm;

          GestoPerfiles gp;
       public RegistroGeneral rg;

       public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

        Point LimiteSuperior = new Point (58,10);      
        Point LimiteInferior =  new Point(170,12);

        string PerfilNuevo = "";

         public MenuNuevoPerfil(GestorMenus g, RegistroGeneral r, GestoPerfiles gestoPerfiles1){
          gm = g;
          rg = r;
          gp = gestoPerfiles1;
         }
         
         public bool Show(bool bActive){
            itsActive = bActive;
              for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");
           CrearDivision();
           Console.ForegroundColor = SelectColor;
           Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y +1);
           Console.Write("Nuevo Perfil");
           Console.SetCursorPosition(LimiteSuperior.X + 16, LimiteSuperior.Y +1);
           Console.Write(PerfilNuevo);
           Console.ForegroundColor = MyColor;
           return false;
         }

          public void CrearDivision(){
          for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
             
             Console.SetCursorPosition(LimiteSuperior.X + 15, i);
              Console.Write("║");
          }
           Console.SetCursorPosition(LimiteSuperior.X + 15, LimiteSuperior.Y );
                Console.Write("╦");
            
                Console.SetCursorPosition(LimiteSuperior.X + 15, LimiteInferior.Y );
                Console.Write("╩");
         
          }

       
        public void Input(ConsoleKeyInfo tecla){
         if(tecla.Key == ConsoleKey.RightArrow){
           if(!string.IsNullOrEmpty(PerfilNuevo)){
           if(!confirmarExistencia()){
            gp.EscribirArchivo(PerfilNuevo);
            gm.GeneralReset();
           }
           else{
            
           }
           }
           else{
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y  + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Debes rellenar todos los campos");
            Thread.Sleep(1000);
            Console.Clear();
           }
         }
         else if(tecla.Key== ConsoleKey.LeftArrow){
           Console.Clear();
           gm.SetMenu(2,0);
           Reset();
         }
       
        else if(char.IsLetter(tecla.KeyChar)){
          if(PerfilNuevo.Length < 21)
          PerfilNuevo += tecla.KeyChar;

        }
        else if(tecla.Key == ConsoleKey.Backspace){
          if(!string.IsNullOrEmpty(PerfilNuevo)){
            PerfilNuevo = PerfilNuevo.Substring(0, PerfilNuevo.Length -1);
            Console.Clear();
            
          }
        }
        else if(tecla.Key == ConsoleKey.Spacebar){
          
            PerfilNuevo+= " ";
          
        }
        }

        bool confirmarExistencia(){
          
          /*for(int i = 0; i < rg.roles.Count; i++){
            if(PerfilNuevo == rg.CualRol(i)){
              return true;
            }
          }*/
          return false;
        }
        public void Reset(){
          PerfilNuevo = "";
        }
      }

      public class MenuLeerPerfiles: Imenu{
        public bool itsActive {get; set;}

          Point LimiteSuperior = new Point(58,7);

          Point LimiteInferior = new Point(170,16);

          GestorMenus gestorMenus;
          RegistroGeneral registroGeneral;

          GestoPerfiles gestoPerfiles;

          public ConsoleColor  MyColor;

          public ConsoleColor SelectColor;

          public ConsoleColor colorTitulo;

          string[] NombresArchivos;

          public int index = 1;

          int desplazamientoMenu;


          public MenuLeerPerfiles(GestorMenus gm, RegistroGeneral rg, GestoPerfiles gp){
           gestorMenus = gm;
           registroGeneral = rg;
           gestoPerfiles = gp;

          }
       public bool Show(bool bActive){
         itsActive = bActive;
         NombresArchivos = gestoPerfiles.ObtenerNombresArchivos();
         if(NombresArchivos.Length > 7){
          Console.Clear();  
         }

       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         if(NombresArchivos.Length <= 7){  
         for(int i = 0; i <= NombresArchivos.Length; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "Selecciona el perfil";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0){
           textoAImprimir += NombresArchivos[NombresArchivos.Length -i];
          }
          
          Console.WriteLine(textoAImprimir);

         }
         }
         else{
            for( int i = 0 + desplazamientoMenu; i < 8 + desplazamientoMenu; i++){
            Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1) - desplazamientoMenu);
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0 + desplazamientoMenu){
            textoAImprimir = "Selecciona la ruta del archivo";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0 + desplazamientoMenu){
           textoAImprimir += NombresArchivos[NombresArchivos.Length -i];
          }
          
          Console.WriteLine(textoAImprimir);
          }
         }

        return false;
       }

       
        public void Input(ConsoleKeyInfo tecla){ 
          if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <=  NombresArchivos.Length ){
                index -= 1;
                if(index == 0 + desplazamientoMenu && desplazamientoMenu != 0){
                  desplazamientoMenu --;
                }
            }
            else{
                index =  NombresArchivos.Length;
                desplazamientoMenu = NombresArchivos.Length - 7;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index <  NombresArchivos.Length ){
          if(index == 7 + desplazamientoMenu && index < NombresArchivos.Length){
                  desplazamientoMenu ++;
                }
                index += 1;
            }
            else{
                index = 1;
                desplazamientoMenu = 0;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorMenus.SetMenu(2,0);
            Reset();
        }
        }

        void Output(){
           gestoPerfiles.LeerArchivo(NombresArchivos[NombresArchivos.Length -index]);
        }
        public void Reset(){
           index = 1;
           desplazamientoMenu =0;
        }

      }
      public class MenuVerUsuarios : Imenu {
        RegistroGeneral registroGeneral1;

        RegistroDeTirada miRegistroDeTirada;
        GestorMenus gestorMenus1;

      public ConsoleColor  MyColor = ConsoleColor.White;

      public ConsoleColor SelectColor = ConsoleColor.Red;

      public ConsoleColor colorTitulo = ConsoleColor.Yellow;

        Point LimiteSuperior = new Point (58,3);

        Point LimiteSuperiorEscritura = new Point(58,8);
        Point LimiteInferior =  new Point(180,16);

        int index = 0;
        int desplazamientoMenu = 0;

        string titulo = "Estudiantes";
        public MenuVerUsuarios(GestorMenus gm, RegistroGeneral rg){
          registroGeneral1 = rg;
          gestorMenus1 = gm;
          
          
        }
       
        public bool itsActive {get; set;}
        public bool Show(bool bActive)
        {
          itsActive = bActive;
          Console.Clear();
          
          for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");
          
           Console.SetCursorPosition(LimiteSuperior.X + 56 -(titulo.Length/2), LimiteSuperior.Y + 1 );
           Console.ForegroundColor = ConsoleColor.Yellow;
           Console.Write(titulo);
           Console.ForegroundColor = ConsoleColor.White;
           MakeFrontier();
           MakeColums();
          if(registroGeneral1.ListadoDeEstudiantes.Count <= 8){
           for(int i = 0; i < registroGeneral1.ListadoDeEstudiantes.Count; i++){
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 1, LimiteSuperiorEscritura.Y + i);
            Console.Write(i);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 11, LimiteSuperiorEscritura.Y + i);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i]);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 51, LimiteSuperiorEscritura.Y + i);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i].Apellido);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 91, LimiteSuperiorEscritura.Y + i);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i].Matricula);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 106, LimiteSuperiorEscritura.Y + i);
            Console.Write($"{registroGeneral1.ListadoDeEstudiantes[i].rolesId.Count}/{registroGeneral1.roles.Count}");
            
           }
         }
         else{
          for(int i = 0 + desplazamientoMenu; i < 8 + desplazamientoMenu; i++){
           if(index == i){
            Console.ForegroundColor = ConsoleColor.Red;
           }
           
            
           
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 1, LimiteSuperiorEscritura.Y + i - desplazamientoMenu);
            Console.Write(i);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 11, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i].Nombre);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 51, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i].Apellido);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 91, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write(registroGeneral1.ListadoDeEstudiantes[i].Matricula);
            Console.SetCursorPosition(LimiteSuperiorEscritura.X + 106, LimiteSuperiorEscritura.Y + i- desplazamientoMenu);
            Console.Write($"{registroGeneral1.ListadoDeEstudiantes[i].rolesId.Count}/{registroGeneral1.roles.Count}");
            Console.ForegroundColor = ConsoleColor.White;
           }
         }

         return false;
        }

        void MakeFrontier(){
           for(int i = LimiteSuperior.X + 1 ; i <= LimiteInferior.X - 1; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y + 2);
              Console.Write("═");                       
           }
           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y +2);
           Console.Write("╠");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y +2);
           Console.Write("╣");

           
        }

        void MakeColums(){
           for(int i = LimiteSuperior.Y + 2; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X + 10 , i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteSuperior.X + 50 , i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteSuperior.X + 90, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteSuperior.X + 105, i);
              Console.Write("║");
           }
               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteSuperior.Y + 2);
              Console.Write("╦");
              Console.SetCursorPosition(LimiteSuperior.X + 50 , LimiteSuperior.Y + 2);
              Console.Write("╦");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteSuperior.Y + 2);
              Console.Write("╦");
              Console.SetCursorPosition(LimiteSuperior.X + 105, LimiteSuperior.Y + 2);
              Console.Write("╦");

               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteInferior.Y );
              Console.Write("╩");
              Console.SetCursorPosition(LimiteSuperior.X + 50 ,LimiteInferior.Y );
              Console.Write("╩");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteInferior.Y );
              Console.Write("╩");
              Console.SetCursorPosition(LimiteSuperior.X + 105, LimiteInferior.Y );
              Console.Write("╩");
              
              Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + 3);
              Console.WriteLine("ID");
              Console.SetCursorPosition(LimiteSuperior.X + 11, LimiteSuperior.Y + 3);
              Console.WriteLine("Nombre");
              Console.SetCursorPosition(LimiteSuperior.X + 51, LimiteSuperior.Y + 3);
              Console.WriteLine("Apellido");
              Console.SetCursorPosition(LimiteSuperior.X + 91, LimiteSuperior.Y + 3);
              Console.WriteLine("Matricula");
              Console.SetCursorPosition(LimiteSuperior.X + 106, LimiteSuperior.Y + 3);
              Console.WriteLine("roles");

               for(int i = LimiteSuperior.X + 1 ; i <= LimiteInferior.X - 1; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y + 4);
              Console.Write("═");
             
              


           }
           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y +4);
           Console.Write("╠");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y +4);
           Console.Write("╣");

              
               Console.SetCursorPosition(LimiteSuperior.X + 10 , LimiteSuperior.Y + 4 );
              Console.Write("╬");
              Console.SetCursorPosition(LimiteSuperior.X + 50 ,LimiteSuperior.Y + 4  );
              Console.Write("╬");
              Console.SetCursorPosition(LimiteSuperior.X + 90, LimiteSuperior.Y + 4  );
              Console.Write("╬");
               Console.SetCursorPosition(LimiteSuperior.X + 105, LimiteSuperior.Y + 4  );
              Console.Write("╬");

              

              




        }

        
       
        public void Input(ConsoleKeyInfo tecla){
           if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 0 && index <= registroGeneral1.ListadoDeEstudiantes.Count -1){
                index -= 1;
                if(index == 0 + desplazamientoMenu && desplazamientoMenu != 0){
                  desplazamientoMenu --;
                }
            }
            else{
               // index = registroGeneral1.ListadoDeEstudiantes.Count -1;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 0 && index < registroGeneral1.ListadoDeEstudiantes.Count -1 ){
                if(index == 7 + desplazamientoMenu && index < registroGeneral1.ListadoDeEstudiantes.Count -1){
                  desplazamientoMenu ++;
                }
                index += 1;
            }
            else{
                index = 0;
                desplazamientoMenu = 0;
            }
        } 
         
         else if(tecla.Key == ConsoleKey.LeftArrow){
            Console.Clear();
            
            
              Console.Clear();
              gestorMenus1.SetMenu(2,0);
              Reset();
            
          }

        }
        public void Reset(){
         index = 0;
         desplazamientoMenu = 0;
        }


      }


public class MenuSobreescribirPerfiles: Imenu{
        public bool itsActive {get; set;}

          Point LimiteSuperior = new Point(58,7);

          Point LimiteInferior = new Point(170,16);

          GestorMenus gestorMenus;
          RegistroGeneral registroGeneral;

          GestoPerfiles gestoPerfiles;

          public ConsoleColor  MyColor;

          public ConsoleColor SelectColor;

          public ConsoleColor colorTitulo;

          string[] NombresArchivos;

          public int index = 1;

          int desplazamientoMenu;


          public MenuSobreescribirPerfiles(GestorMenus gm, RegistroGeneral rg, GestoPerfiles gp){
           gestorMenus = gm;
           registroGeneral = rg;
           gestoPerfiles = gp;

          }
       public bool Show(bool bActive){
         itsActive = bActive;
         NombresArchivos = gestoPerfiles.ObtenerNombresArchivos();
         if(NombresArchivos.Length > 7){
          Console.Clear();  
         }


       if(bActive){
         MyColor = ConsoleColor.White; 
         SelectColor = ConsoleColor.Red;
         colorTitulo = ConsoleColor.Yellow;
       }
       else{
         MyColor = ConsoleColor.Gray;
         SelectColor = ConsoleColor.DarkRed;
         colorTitulo = ConsoleColor.DarkGray;
       }

       for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           //Escribir menu
         if(NombresArchivos.Length <= 7){  
         for(int i = 0; i <= NombresArchivos.Length; i++){
          Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1));
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0){
            textoAImprimir = "Selecciona la ruta del archivo";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0){
           textoAImprimir += NombresArchivos[NombresArchivos.Length -i];
          }
          
          Console.WriteLine(textoAImprimir);

         }
         }
         else{
            for( int i = 0 + desplazamientoMenu; i < 8 + desplazamientoMenu; i++){
            Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + (i+1) - desplazamientoMenu);
          string textoAImprimir = "";
          Console.ForegroundColor = MyColor;
          if(i == index){
             textoAImprimir += ">";
            Console.ForegroundColor = SelectColor;
          }
          else if(i == 0 + desplazamientoMenu){
            textoAImprimir = "Selecciona la ruta del archivo";
             Console.ForegroundColor = colorTitulo;
          }
          else{
            textoAImprimir = " ";
          }
          if(i != 0 + desplazamientoMenu){
           textoAImprimir += NombresArchivos[NombresArchivos.Length -i];
          }
          
          Console.WriteLine(textoAImprimir);
          }
         }

        return false;
       }

       
        public void Input(ConsoleKeyInfo tecla){ 
          if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(index > 1 && index <=  NombresArchivos.Length ){
                index -= 1;
                if(index == 0 + desplazamientoMenu && desplazamientoMenu != 0){
                  desplazamientoMenu --;
                }
            }
            else{
                index =  NombresArchivos.Length;
                desplazamientoMenu = NombresArchivos.Length - 7;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(index >= 1 && index <  NombresArchivos.Length ){
          if(index == 7 + desplazamientoMenu && index < NombresArchivos.Length){
                  desplazamientoMenu ++;
                }
                index += 1;
            }
            else{
                index = 1;
                desplazamientoMenu = 0;
            }
        } 
        else if(tecla.Key == ConsoleKey.RightArrow){

          Output();
        }
        else if(tecla.Key == ConsoleKey.LeftArrow){
          Console.Clear();
            gestorMenus.SetMenu(2,0);
        }
        }

        void Output(){
           gestoPerfiles.Sobreescribir(NombresArchivos[NombresArchivos.Length -index]);
           gestorMenus.GeneralReset();
        }
        public void Reset(){
           
        }

      }

      public class MenuMensaje {
        Point LimiteSuperior = new Point(3,17);

        Point LimiteInferior = new Point(170,20);
        public bool mostrando;


        ConsoleColor MyColor = ConsoleColor.White; 
        ConsoleColor SelectColor = ConsoleColor.Green;
        ConsoleColor colorTitulo = ConsoleColor.Yellow;
        public void show(string Mensaje){
          mostrando = true;
         for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(i, LimiteSuperior.Y);
              Console.Write("═");
              Console.SetCursorPosition(i, LimiteInferior.Y);
              Console.Write("═");
              


           }

            for(int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++){
              Console.ForegroundColor = MyColor;
             
              Console.SetCursorPosition(LimiteSuperior.X, i);
              Console.Write("║");
              Console.SetCursorPosition(LimiteInferior.X, i);
              Console.Write("║");
           }

           Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
           Console.Write("╔");
           Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
           Console.Write("╚");
           Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
           Console.Write("╗");
           Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
           Console.Write("╝");

           Console.SetCursorPosition(LimiteSuperior.X + 1, LimiteSuperior.Y + 1);
           Console.ForegroundColor = colorTitulo;
           Console.Write("Mensaje: ");
            Console.ForegroundColor = SelectColor;
           Console.Write(Mensaje);

        }
      }



    }


    
 
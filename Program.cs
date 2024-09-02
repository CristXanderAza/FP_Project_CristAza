// See https://aka.ms/new-console-template for more information
using Ruleta;
using System.Drawing;

//Menu menu = new Menu(170, 60, ConsoleColor.Black, new Point (3,3), new Point(28,16));

GestorMenus gestorMenus = new GestorMenus();
Console.CursorVisible = false;
Imenu menuX = new MenuPrincipal(gestorMenus);

RegistroGeneral registroGeneral = new RegistroGeneral();

GestoPerfiles gestoPerfiles = new GestoPerfiles(registroGeneral);

Imenu[] menuY = new Imenu[6];
Imenu[] menuZ = new Imenu[8];



menuZ[0] = new PhMenu();

menuY[0] = new PhMenu();



menuY[5] = new MenuSalida(gestorMenus);

menuY[1] = new menuVotacion(registroGeneral, gestorMenus);

menuY[2] = new MenuRegistrosView(gestorMenus, registroGeneral);

menuY[3] = new MenuAjustes(gestorMenus);

menuY[4] = new MenuPerfiles(gestorMenus, registroGeneral);

menuZ[1] = new MenuRegistroUno(gestorMenus, registroGeneral);
menuZ[2] = new MenuNuevoUsuario(gestorMenus, registroGeneral);

menuZ[3] = new MenuNuevoRol(gestorMenus, registroGeneral);

menuZ[4] = new MenuNuevoPerfil(gestorMenus,registroGeneral, gestoPerfiles);

menuZ[5] = new MenuLeerPerfiles(gestorMenus, registroGeneral, gestoPerfiles);

menuZ[6] = new MenuVerUsuarios(gestorMenus, registroGeneral);
menuZ[7] = new MenuSobreescribirPerfiles(gestorMenus, registroGeneral, gestoPerfiles);

MenuMensaje menuMensaje = new MenuMensaje();


/*
int indice = 1;

List<string> menuTxt = new List<string>(){"Menu", "Tirar", "Registro De tiradas", "Ajustes", "Perfiles", "Salir"};

while(true){
ConsoleKeyInfo tecla = Console.ReadKey();
 
 
     if (tecla.Key == ConsoleKey.UpArrow)
        {

            
             if(indice > 1 && indice <= 5 ){
                indice -= 1;
            }
            else{
                indice = 5;
            }
            
            
        }
        else if (tecla.Key == ConsoleKey.DownArrow)
        { if(indice >= 1 && indice < 5 ){
                indice += 1;
            }
            else{
                indice = 1;
            }
        }
       



menu.DibujarUnMarco(menuTxt, indice);

Console.SetCursorPosition(5,19);
}
*/
gestoPerfiles.LeerArchivo(@"C:\Users\pluto\OneDrive\Escritorio\projects\Ruleta\Perfiles\Default.txt");
Console.Clear();
do{

  if(gestorMenus.Reset){
    Console.Clear();
    menuY[5].Reset();

menuY[1].Reset();

menuZ[1].Reset();

menuY[2].Reset();

menuY[3].Reset();

menuZ[1].Reset();
menuZ[2].Reset();

gestorMenus.Reset = false;
  }

DrawTitle();

if(registroGeneral.Mensaje != null){
 menuMensaje.show(registroGeneral.Mensaje);
 //Thread.Sleep(1000);
 registroGeneral.Mensaje = null;
}
else if(menuMensaje.mostrando){
  Console.Clear();
  menuMensaje.mostrando = false;
}
 bool mZActive = menuZ[gestorMenus.IZ].Show(true);
  bool mYActive = menuY[gestorMenus.IY].Show(mZActive);
  menuX.Show(mYActive);
if(gestorMenus.itsListening){
  if(menuZ[gestorMenus.IZ].itsActive){
   
   menuZ[gestorMenus.IZ].Input(Console.ReadKey());
  }
  else if(menuY[gestorMenus.IY].itsActive){
    menuY[gestorMenus.IY].Input(Console.ReadKey());
  
  }
  else{
  menuX.Input(Console.ReadKey());
  }}
  /* dep
  Console.SetCursorPosition(25,25);
  foreach (Estudiante e in registroGeneral.ListadoDeEstudiantes){
    string s = "";
    foreach(int i in e.rolesId){
      s += registroGeneral.CualRol(i);
    }
    Console.WriteLine(e.Nombre + "" + s);
  }
  */

 

}while(!(gestorMenus.Exit));
Console.ForegroundColor = ConsoleColor.White;

static void DrawTitle(){
   string Title = "<<Ruleta_Cristopher-Xander-Aza-Diaz_2023-0731 (FP-Orison Soto : Grupo 13)>>";
   Console.SetCursorPosition(Console.WindowWidth/2 - (Title.Length/2), 1);
   Console.ForegroundColor = ConsoleColor.Green;
   Console.Write(Title);
   Console.ForegroundColor = ConsoleColor.White;

}



public class GestorMenus {


public Utilidades utilidades = new Utilidades();
public int IY = 0;

public bool Reset;
public bool itsListening = true;
public bool Exit = false;

public bool votacionViable = true;
public int IZ = 0;
public void SetMenu( int position, int menu){
    if(position == 1){
      IY = menu;
    }
    else if(position ==2){
      IZ = menu;

    }
    else{
      IY = menu;
      IZ = menu;
    }


}

public void GeneralReset(){
  Reset = true;
  SetMenu(3,0);
  Console.ForegroundColor = ConsoleColor.White;
}

public void setExit(){

  Exit = true;
}
}
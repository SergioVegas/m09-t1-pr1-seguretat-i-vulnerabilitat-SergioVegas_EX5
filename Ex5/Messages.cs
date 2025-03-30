using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex5
{
    public static class Messages
    {
        public static string Menu = "Menú Principal\n"+ "1. Registre d'usuari\n" + "2. Verificació de dades\n" + "3. Encriptació/Desencriptació RSA\n" + "4. Sortir\n" + "Selecciona una opció:";
        public static string InvalidOption = "Opció no vàlida. Torna a intentar.";
        public static string UserName = "Introdueix el nom d'usuari:";
        public static string UserPassword = "Introdueix la contrasenya:";
        public static string UserCorrect = "Usuari registrat correctament.";
        public static string CorrectData = "Les dades són correctes. Accés permès.";
        public static string IncorrectData = "Les dades no coincideixen. Accés denegat.";
    }
}

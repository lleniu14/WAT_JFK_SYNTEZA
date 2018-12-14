using System;

namespace lenkiewicz {
    class Class1 {

        string s = "Pusty string";
        string x = "Pusty string";

        void MyMethod() {

           if(s == null) {
                string x = "Pusty string";
                string y = "Niepusty string";
            } else if(x == null) {
                string a = "Pusty string";
                string b = "Pusty string";
                string c = "Niepusty string";
            }

            {
                string o = "Pusty string";
                o = "Juz niepusty";
                string k = "Pusty string";
                string s = "Pusty string";
            }


        }  

        public static void Main() {



        }

    }
}

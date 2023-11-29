namespace registers
{
    class Program
    {
        public static void Main()
        {
            string[] lines = File.ReadAllLines("/Users/kramerbalazs/Downloads/C#/registers/registers/input.txt"); // file beolvasasa egy string tombbe 
            
            string[] initialRegisters = lines[0].Split(','); // kezdeti allapotok mentese egy string tombbe szeparator ','
            
            int[] registers = new int[4]; // uj int tomb letrehozasa (new int[4] -> 4 elemu ures tomb)

            for (int i = 0; i < initialRegisters.Length; i++) // a initialRegisters string tombon for loop 
            {
                registers[i] = int.Parse(initialRegisters[i]); // a registers int tombbe int-kent atmasolom az initialRegister erteket
            }
            
            for (int i = 1; i < lines.Length; i++) // for loop a maradek filebol olvasott tartalomra soronkent
            {
                string[] op = lines[i].Split(' '); // az adott sor felbontasa egy tombbe
                int feedback = executeTask(registers, op); // int-kent eltarolom hogy kell-e visszaugrani
                if (feedback != -1) // a visszaugras kereset azzal ellenorzom, hogy a feedback int -1-e (akkor kell ugrani ha nem -1)
                {
                    i = feedback; // ha nem -1 volt a feedback akkor a feedbackben visszaadott sorra allitom az i erteket, igy a for loop onnan folytatja
                }
            }
            
            
            File.WriteAllText("/Users/kramerbalazs/Downloads/C#/registers/registers/output.txt", $"{registers[0]},{registers[1]},{registers[2]},{registers[3]}"); // registerek erteket kiirom az output.txt file-ba
            
        }

        private static int executeTask(int[] initialRegisters, string[] operation) // int[] a regiszterek, string[] az utasitas
        {

            string task = operation[0]; // az utasitas tomb 0. eleme megadja hogy mi az utasitas (MOV, ADD, SUB, JNE)

            int dest, src, val, lho, rho; // minden kesobb felhasznalt valtozo inicializalasa
            switch (task) // a task string alapjan egy switch 
            {
                case "MOV": // ha MOV a task 
                    src = getIndex(operation[2]); // megnezem az operation[2]-t, hogy valos karakter e vagy egy szam
                    dest = getIndex(operation[1]); // megnezem az operation[1]-t, melyik indexet kell majd beallitani
                    if (src == -1) // ha az src -1 akkor tudom, hogy nem karakter volt hanem szam
                    {
                        val = int.Parse(operation[2]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        val = initialRegisters[src]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    initialRegisters[dest] = val; // vegrehajtom a MOV-t es beallitom a kert erteket
                    return -1;  // -1-el terek vissza mivel nem igenyel visszaugrast a muvelet
                case "ADD":
                    src = getIndex(operation[2]); // megnezem az operation[2]-t, hogy valos karakter e vagy egy szam
                    dest = getIndex(operation[1]); // megnezem az operation[1]-t, melyik indexet kell majd beallitani
                    val = getIndex(operation[3]); // megnezem az operation[3]-t, hogy valos karakter e vagy egy szam
                    if (src == -1) // ha az src -1 akkor tudom, hogy nem karakter volt hanem szam
                    {
                        lho = int.Parse(operation[2]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        lho = initialRegisters[src]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    if (val == -1)
                    {
                        rho = int.Parse(operation[3]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        rho = initialRegisters[val]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    initialRegisters[dest] = lho + rho; //vegrehajtom az ADD-t es beallitom a kert erteket
                    return -1;  // -1-el terek vissza mivel nem igenyel visszaugrast a muvelet
                case "SUB":
                    src = getIndex(operation[2]); // megnezem az operation[2]-t, hogy valos karakter e vagy egy szam
                    dest = getIndex(operation[1]); // megnezem az operation[1]-t, melyik indexet kell majd beallitani
                    val = getIndex(operation[3]); // megnezem az operation[3]-t, hogy valos karakter e vagy egy szam
                    if (src == -1) // ha az src -1 akkor tudom, hogy nem karakter volt hanem szam
                    {
                        lho = int.Parse(operation[2]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        lho = initialRegisters[src]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    if (val == -1)
                    {
                        rho = int.Parse(operation[3]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        rho = initialRegisters[val]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    initialRegisters[dest] = lho - rho; //vegrehajtom az ADD-t es beallitom a kert erteket
                    return -1;  // -1-el terek vissza mivel nem igenyel visszaugrast a muvelet
                case "JNE":
                    src = getIndex(operation[2]); // megnezem az operation[2]-t, hogy valos karakter e vagy egy szam
                    val = getIndex(operation[3]); // megnezem az operation[3]-t, hogy valos karakter e vagy egy szam
                    if (val == -1) 
                    {
                        val = int.Parse(operation[3]); // mivel szam ezert int-e alakitom, hogy kesobb felhasznaljam
                    }
                    else
                    {
                        val = initialRegisters[val]; // ha karater volt akkor a getindex() megmondta, hogy melyik indexu regiszter-t kell olvasom
                    }
                    if (val == initialRegisters[src]) // ha a megadtott regiszter erteke azonos a megadott ertekkel akkor nem kell visszaugrani
                    {
                        return -1; // -1-el terek vissza mivel nem igenyel visszaugrast a muvelet
                    }
                    return int.Parse(operation[1]);  // az int-te alakitott operation[2]-vel terek vissza mivel igenyel visszaugrast a muvelet
            }

            return -1; // ha barmi felrecsuszna biztositekkent -1-el visszaterek de ide sosem jut el a fuggveny

        }

        private static int getIndex(string s) // egy string parameter van ami a regiszter neve lesz vagy egy szam
        {
            switch (s) // parameterre switch
            {
                case "A":
                    return 0; // ha a parameter A akkor a regiszterek tombjenek a 0. elemet kell felhasznalnom
                case "B":
                    return 1; // ha a parameter B akkor a regiszterek tombjenek a 1. elemet kell felhasznalnom
                case "C":
                    return 2; // ha a parameter C akkor a regiszterek tombjenek a 2. elemet kell felhasznalnom
                case "D":
                    return 3; // ha a parameter D akkor a regiszterek tombjenek a 3. elemet kell felhasznalnom
            }

            return -1; // ha nem talalt egyezest a switch-ben akkor nem karater volt igy -1-el terek vissza
        }
    }
}

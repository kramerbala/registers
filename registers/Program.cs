namespace registers
{
    class Program
    {
        public static void Main()
        {
            string[] lines = File.ReadAllLines("/Users/kramerbalazs/RiderProjects/registers/registers/input.txt");
            
            string[] initialRegisters = lines[0].Split(',');
            int A = int.Parse(initialRegisters[0]);
            int B = int.Parse(initialRegisters[1]);
            int C = int.Parse(initialRegisters[2]);
            int D = int.Parse(initialRegisters[3]);
            
            List<string[]> operations = new List<string[]>();
            
            for (var i = 1; i < lines.Length; i++)
            {
                string[] op = lines[i].Split(' ');
                operations.Add(op);
            }

            int[] registers = new int[4];

            for (int i = 0; i < initialRegisters.Length; i++)
            {
                registers[i] = int.Parse(initialRegisters[i]);
            }
            
            for (var i = 0; i < operations.Count; i++)
            {
                var feedback = executeTask(registers, operations[i]);
                if (feedback != -1)
                {
                    i = feedback;
                }
            }
            
            
            File.WriteAllText("/Users/kramerbalazs/RiderProjects/registers/registers/output.txt", $"{registers[0]},{registers[1]},{registers[2]},{registers[3]}");
            
        }

        private static int executeTask(int[] initialRegisters, string[] operation)
        {

            string task = operation[0];

            int dest, src, val, lho, rho;
            switch (task)
            {
                case "MOV":
                    src = getIndex(operation[2]);
                    dest = getIndex(operation[1]);
                    if (src == -1)
                    {
                        val = int.Parse(operation[2]);
                    }
                    else
                    {
                        val = initialRegisters[src];
                    }
                    initialRegisters[dest] = val;
                    return -1;
                case "ADD":
                    src = getIndex(operation[2]);
                    dest = getIndex(operation[1]);
                    val = getIndex(operation[3]);
                    if (src == -1)
                    {
                        lho = int.Parse(operation[2]);
                    }
                    else
                    {
                        lho = initialRegisters[src];
                    }
                    if (val == -1)
                    {
                        rho = int.Parse(operation[3]);
                    }
                    else
                    {
                        rho = initialRegisters[val];
                    }
                    initialRegisters[dest] = lho + rho;
                    return -1;
                case "SUB":
                    src = getIndex(operation[2]);
                    dest = getIndex(operation[1]);
                    val = getIndex(operation[3]);
                    if (src == -1)
                    {
                        lho = int.Parse(operation[2]);
                    }
                    else
                    {
                        lho = initialRegisters[src];
                    }
                    if (val == -1)
                    {
                        rho = int.Parse(operation[3]);
                    }
                    else
                    {
                        rho = initialRegisters[val];
                    }
                    initialRegisters[dest] = lho - rho;
                    return -1;
                case "JNE":
                    src = getIndex(operation[2]);
                    val = getIndex(operation[3]);
                    if (val == -1)
                    {
                        val = int.Parse(operation[3]);
                    }
                    else
                    {
                        val = initialRegisters[val];
                    }
                    if (val == initialRegisters[src])
                    {
                        return -1;
                    }
                    return int.Parse(operation[1]) - 1;
            }

            return -1;

        }

        private static int getIndex(string s)
        {
            switch (s)
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
            }

            return -1;
        }
    }
}
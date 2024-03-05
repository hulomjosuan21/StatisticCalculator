namespace CalStat
{
    internal class Program
    {
        private static Program? instance;
        public int[,] ArrVal { get; set; }
        private Program(){}

        public static Program Instance
        {
            get
            {
                return (instance == null) ? new Program() : instance;
            }
        }
        public int GET_N
        {
            get
            {
                return ArrVal.GetLength(0) * ArrVal.GetLength(1);
            }
        }
        public int GET_LV
        {
            get
            {
                int min = ArrVal[0,0];

                for(int row = 0; row < ArrVal.GetLength(0); row++){
                    for(int col = 0; col < ArrVal.GetLength(1); col++){
                        if(ArrVal[row,col] < min){
                            min = ArrVal[row,col];
                        }
                    }
                }

                return min;
            }
        }
        public int GET_HV
        {
            get
            {
                int min = ArrVal[0,0];

                for(int row = 0; row < ArrVal.GetLength(0); row++){
                    for(int col = 0; col < ArrVal.GetLength(1); col++){
                        if(ArrVal[row,col] > min){
                            min = ArrVal[row,col];
                        }
                    }
                }

                return min;
            }
        }
        public int GET_STEP1
        {
            get
            {
                return GET_HV - GET_LV;
            }
        }
        public int GET_STEP2
        {
            get
            {
                double res = 1 + 3.322 * (Math.Log10(GET_N));
                return (int) Math.Round(res,MidpointRounding.AwayFromZero);
            }
        }
        public int[] GET_STEP3
        {
            get
            {
                double step3 = GET_STEP1 / GET_STEP2;
                return new int[]{(int)step3,(int)step3+1};
            }
        }
        public int[,] GET_STEP4_CLASSES
        {
            get
            {
                int[] arr_a = new int[GET_STEP2];
                arr_a[0] = GET_LV;
                int[] arr_b = new int[GET_STEP2];
                for(int a = 1; a < GET_STEP2; a++){
                    if(a == 1){
                        arr_a[a] = GET_LV + GET_STEP3[1];
                    }
                    else
                    {
                        arr_a[a] = arr_a[a-1] + GET_STEP3[1];
                    }
                }
                for(int a = 0; a < GET_STEP2; a++){
                    if(a == 0){
                        arr_b[a] = GET_LV + GET_STEP3[0];
                    }
                    else
                    {
                        arr_b[a] = arr_a[a] + GET_STEP3[0];
                    }
                }
                int[,] combinedArr = new int[2,GET_STEP2];
                for(int b = 0; b < GET_STEP2; b++){
                    combinedArr[0,b] = arr_a[b];
                    combinedArr[1,b] = arr_b[b];
                }
                return combinedArr;
            }
        }
        public string[] GET_STEP4_TALLY{
            get{
                int columnCount = ArrVal.GetLength(0);
                int rowCount = ArrVal.GetLength(1);
                string[] getTally = new string[GET_STEP2];
                for(int i = 0; i < GET_STEP2; i++){
                    int a = GET_STEP4_CLASSES[0,i];
                    int b = GET_STEP4_CLASSES[1,i];

                   for(int j = 0; j < columnCount; j++){
                        string h_a = "";
                        for(int k = 0; k < rowCount; k++){
                            if(a >= ArrVal[j,k] && b <= ArrVal[j,k]){
                                h_a += "|";
                            }
                        }
                        getTally[i] = h_a;
                   }
                }
                return getTally;
            }
        }
        private void DisplayData(){
            int columnCount = ArrVal.GetLength(0);
            int rowCount = ArrVal.GetLength(1);

            for(int row = 0; row < rowCount; row++){
                for(int col = 0; col < GET_STEP2; col++){
                    if(col != columnCount-1){
                        Console.Write(" {0} :",ArrVal[row,col]);
                    }else Console.Write(" {0} ",ArrVal[row,col]);;
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Program prog = Program.Instance;

            //prog.ArrVal = new int[,]{{75,80,85,81,90,93},
                                    //{86,77,95,88,75,91},
                                    //{79,84,87,81,94,76},
                                    //{87,86,88,86,95,88},
                                    //{79,89,93,88,81,95},
                                    //{81,88,87,86,88,89},
                                    //{95,77,88,77,93,77},
                                    //{88,75,93,87,96,86}};

            prog.ArrVal = new int[,]{{20,25,30,36,38,40,40,42},
                                    {45,48,50,53,56,58,60,64},
                                    {66,68,70,72,74,80,88,90},
                                    {96,100,120,130,135,140,150,152}};


            //prog.DisplayData();
            Console.WriteLine("Step 1: {0}\nStep 2: {1}",prog.GET_STEP1,prog.GET_STEP2);
            Console.WriteLine("Step 3: {0} : {1}",prog.GET_STEP3[0],prog.GET_STEP3[1]);

            Console.WriteLine("Step 4:");
            Console.WriteLine("Classes:");
            for(int i = 0; i < prog.GET_STEP2; i++){
                Console.WriteLine("{0} - {1}",prog.GET_STEP4_CLASSES[0,i],prog.GET_STEP4_CLASSES[1,i]);
            }

            Console.WriteLine("Tally: ");
            foreach(string s in prog.GET_STEP4_TALLY){
                Console.WriteLine(s);
            }
        }
    }
}
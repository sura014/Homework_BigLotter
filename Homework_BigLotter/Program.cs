using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_BigLotter
{
    class Program
    {
        struct User_Bet
        {
            public int[] each_bet;
        } 

        static void Main(string[] args)
        {
            int Bet_count;
            while (true)
            {
                string input;
                Console.Write("請輸入下注數量(介於1-5) : ");
                Bet_count = CheckBet();
                User_Bet[] user_bet_list = new User_Bet[Bet_count];
                for(int i = 0; i < Bet_count; i++)
                {
                    User_Bet user_bet = new User_Bet();
                    Console.Write("輸入6個數，其值介於1~49，並以逗點隔開 : ");
                    user_bet.each_bet = CheckInt();
                    user_bet_list[i] = user_bet;
                }                
                Console.Write("本期開獎號碼:");
                int[] com_numbers = Select_Numbers();
                for (int i = 0; i < com_numbers.Length; i++)
                    Console.Write(com_numbers[i] + " ");
                Console.Write("\n");
                for (int i = 0; i < Bet_count; i++)
                {
                    Console.WriteLine($"\n第{i+1}注");
                    Console.Write("使用者投注號碼 : ");
                    for(int j = 0; j < 6; j++)
                    {
                        Console.Write(user_bet_list[i].each_bet[j] + " ");
                    }
                    int[] confirm_number = Confirm_Numbers(user_bet_list[i].each_bet, com_numbers);
                    Console.Write("中獎號碼:");
                    if (confirm_number.Length != 0)
                    {
                        for (int k = 0; k < confirm_number.Length; k++)
                            Console.Write(confirm_number[k] + " ");
                    }
                    else
                        Console.Write("無  ");
                    Console.WriteLine("中獎號碼有" + (confirm_number.Length) + "個");                    
                }                
                Console.Write("\n");
                Console.WriteLine("是否要再玩一次?(Yes:1 No:Any Key)");
                input = Console.ReadLine();
                if (input != "1")
                    break;
            }
            Console.WriteLine("Press Any Key...");   
            Console.ReadKey();
        }

        
        //使用者輸入下注數量and防呆
        public static int CheckBet()
        {
            int Bet = 1;
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out Bet) && Bet >= 1 && Bet <= 5)
                    break;
            }            
            return Bet;
        }
        //使用者輸入樂透號碼and防呆
        public static int[] CheckInt()
        {
            string input = "";
            string[] get_number = { "" };
            int[] user_numbers = new int[6];
            while (true)
            {
                int number = 0;
                bool int_parse_able = false;
                input = Console.ReadLine();
                get_number = input.Split(',', ' ');
                if (get_number.Length == 6)
                    for (int i = 0; i < get_number.Length; i++)
                    {
                        if (!(int.TryParse(get_number[i], out number)))
                        {                            
                            break;
                        }
                        if (int.Parse(get_number[i]) < 1 || int.Parse(get_number[i]) > 49)
                        {                            
                            break;
                        }
                        if (i == get_number.Length - 1)
                            int_parse_able = true; 
                    }
                if (int_parse_able)
                    break;
            }
            for (int i = 0; i < 6; i++)
                user_numbers[i] = int.Parse(get_number[i]);
            return user_numbers;
        }
        //產生1-49的陣列
        public static int[] Creat_Lotter_List()
        {
            int[] lotter_list = new int[49];
            for (int i = 0; i < lotter_list.Length; i++)
                lotter_list[i] = i + 1;
            return lotter_list;
        }
        //從1-49的陣列隨機抽出六個數字
        public static int[] Select_Numbers()
        {
            Random random = new Random();
            int[] lotter_list = Creat_Lotter_List();
            int[] com_select_number = new int[6];
            int counter = 0 ;
            while (counter < 6)
            {
                int random_element = random.Next(0, 49);
                if (lotter_list[random_element] != 0)
                {
                    com_select_number[counter] = lotter_list[random_element];
                    lotter_list[random_element] = 0;
                    counter++;
                }
            }
            return com_select_number;
        }
        //對獎
        public static int[] Confirm_Numbers(int[] user, int[] com)
        {
            int counter = 0;
            int[] confirm_numbers=new int[0];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (user[i] == com[j])
                    {
                        Array.Resize(ref confirm_numbers,confirm_numbers.Length+1);
                        confirm_numbers[counter] = user[i];
                        counter++;
                    }                        
                }            
            return confirm_numbers;
        }
    }
}

using System;
using System.Collections;

public class Program{

  static List<BankAccount> list = [];


  public static void Main(){

    int choice;

    do {

      Console.WriteLine("\n\t\t\tWelcome to Sam's Bank.\n");

      printMenu();

      Console.Write("Enter your choice: ");
      choice = Convert.ToInt32(Console.ReadLine());

      switch(choice){
        case 1:
          Console.Write("Enter your name: ");
          string ? name = Console.ReadLine();

          Console.Write("Enter the amount you want to deposit: ");
          double amount = Convert.ToInt32(Console.ReadLine());

          BankAccount obj = new BankAccount(name, amount);

          Console.WriteLine("\n\tAccount created successfully.");

          list.Add(obj);
        break;

        case 2:
          Console.Write("Enter your name: ");
          string ? temp = Console.ReadLine();

          printBalance(temp);
        break;

        case 3:

          Console.Write("Enter your name: ");
          string ? n = Console.ReadLine();

          
          Console.Write("Enter the amount you want to deposit: ");
          double a = Convert.ToInt32(Console.ReadLine());

          deposit(n, a);

          Console.WriteLine("\n\tDeposited Successfully");

        break;

        case 4:
          Console.Write("Enter your name: ");
          string ? num = Console.ReadLine();

          
          Console.Write("Enter the amount you want to withdraw: ");
          double amt = Convert.ToInt32(Console.ReadLine());

          withdraw(num, amt);

        break;
      }
      

    }while(choice != 5);


  }

  static void withdraw(string name, double amount){

    for(int i = 0 ; i < list.Count ; i++){
      BankAccount obj = list[i];

      if(obj.getName() == name){
        double balance = obj.getBalance() - amount;
        if(amount > obj.getBalance()){
          Console.WriteLine("\n\tYour withdrawal amount is greater than your balance.");
        }else {
          obj.setBalance(balance);
          Console.WriteLine("\n\tWithdrew Successfully");

        }
        
        break;
      }
    }
  }

  static void deposit(string name, double amount){

    for(int i = 0 ; i < list.Count ; i++){
      BankAccount obj = list[i];

      if(obj.getName() == name){
        obj.setBalance(obj.getBalance() + amount);
        break;
      }
    }
  }

  static void printBalance(string name){

    for(int i = 0 ; i < list.Count ; i++){
      BankAccount obj = list[i];

      if(obj.getName() == name){
        Console.WriteLine("\n\tBalance: " + obj.getBalance());
        break;
      }
    }
  }


  static void printMenu(){
    Console.WriteLine("1. Create a bank Account.");
    Console.WriteLine("2. Check Balance.");
    Console.WriteLine("3. Deposit.");
    Console.WriteLine("4. Withdraw.");
    Console.WriteLine("5. Quit.\n");
  }
}

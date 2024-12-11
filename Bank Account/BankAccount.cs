class BankAccount{

  private double balance;
  private string name;

  public BankAccount(String name, double balance){
    this.name = name;
    this.balance = balance;
  }

  public string getName(){
    return this.name;
  }

  public double getBalance(){
    return this.balance;
  }

  public void setBalance(double balance){
    this.balance = balance;
  }

  public string toString(){
    return "\n\tName: " + this.name + " Balance: " + this.balance;
  }


}
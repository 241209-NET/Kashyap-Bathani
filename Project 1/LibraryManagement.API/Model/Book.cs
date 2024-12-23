namespace LibraryManagement.API.Models;

public class Book{

  public int BookId {get; set;}
  public string Title {get; set;} = "";
  public string Author {get; set;} = "";
  public string Isbn {get; set;} = "";

  public List<LendingRecord> LendingRecords {get; set;} = [];

}
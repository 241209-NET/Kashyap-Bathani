namespace LibraryManagement.API.Models;

public class LendingRecord{

  public int RecordId {get; set;}
  public int BookId {get; set;}
  public int MemberId {get; set;}
  public DateTime LendDate {get; set;}
  public DateTime? ReturnDate {get; set;} //Books not returned yet

  public Book? Book {get; set;}
  public Member? Member{get; set;}
  
}
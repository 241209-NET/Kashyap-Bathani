namespace LibraryManagement.API.Models;

public class Member{

  public int MemberId {get; set;}
  public string Name {get; set;} = "";
  public string Email {get; set;} = "";
  public string PhoneNo {get; set;} = "";

  public List<LendingRecord> LendingRecords {get; set; } = [];
}
using CetContact.Models;
namespace CetContact.Data;
using SQLite;

public class CetContactDatabase
{
    private  SQLiteConnection db;

    private void CreateDatabase()
    {
        
        var path = Path.Combine(FileSystem.AppDataDirectory, "contacts.db3");
        
        db=new SQLiteConnection(path);
        db.CreateTable<PhoneContact>();
    }
    public CetContactDatabase()
    {
        CreateDatabase();
    }

    public List<PhoneContact> GetAll()
    {
        return db.Table<PhoneContact>().OrderBy(p => p.Name).ToList();
    }

    
    public void AddContact(PhoneContact contact)
    {
        db.Insert(contact);
        
      
    }

    public void Update(PhoneContact contact)
    {
       db.Update(contact);
        
    }

    public void Delete(int id)
    {
        db.Delete<PhoneContact>(id);

    }
    

}
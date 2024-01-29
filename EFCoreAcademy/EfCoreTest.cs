using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EFCoreAcademy;

public class EfCoreTest : IHostedService
{
    private readonly ApplicationDbContext dbContext;

    public EfCoreTest(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Start();
        Environment.Exit(0);
      
    }

    private async Task Start()
    {
        Console.WriteLine("app started");
        var newId = await Insert();
        Console.WriteLine("new address added, Id : " + newId);

//query one entity
        var ad1 = await GetAddress(1);
        var ad2 = await GetAddress(200000);
        Console.WriteLine("Address with Id 1 : " + JsonSerializer.Serialize(ad1));
        Console.WriteLine("Address with Id 200000 : " + JsonSerializer.Serialize(ad2));

//query all entities
        DisplayAllAddresses();

//query all entities - empty
        DisplayAllStudents();


//update Entity
        var adToUp = await GetAddress(3);
        Console.WriteLine("Address to update : " + JsonSerializer.Serialize(adToUp));
        await UpdateAddress(3);
        adToUp = await GetAddress(3);
        Console.WriteLine("Address after update : " + JsonSerializer.Serialize(adToUp));

//delete
        Console.WriteLine($"Deleting address Id {newId}");
        await DeleteAddress(newId);
        Console.WriteLine("addresses after delete : ");
        DisplayAllAddresses();
    }


    void DisplayAllStudents()
    {
        var students = GetStudents();
        Console.WriteLine("students : ");
        students.ForEach(s => { Console.WriteLine(JsonSerializer.Serialize(s)); });
    }

    async Task<int> Insert()
    {
        var city = GetRdmString(5);
        var houseN = Int32.Parse(GetRdmNumericString(2));
        var street = $"{GetRdmString(4)} {GetRdmString(2)} {GetRdmString(7)}";
        var zip = GetRdmNumericString(5);
        var address = new Address()
        {
            City = city,
            HouseNumber = houseN,
            Street = street,
            Zip = zip
        };
        dbContext.Addresses.Add(address);
        await dbContext.SaveChangesAsync();
        return address.Id;
    }

    string GetRdmString(int numberCharacters)
    {
        var chars = new List<char>();
        for (int i = 0; i < numberCharacters; i++)
        {
            var rdm = new Random();
            chars.Add((char)rdm.Next(65, 90));
        }

        return string.Concat(chars);
    }

    string GetRdmNumericString(int count)
    {
        var res = new List<string>();
        var rdm = new Random();

        for (int i = 0; i < count; i++)
        {
            res.Add(rdm.Next(1, 9).ToString());
        }

        return string.Concat(res);
    }

    void DisplayAllAddresses()
    {
        var addresses = GetAddresses();
        addresses.ForEach(a => { Console.WriteLine(JsonSerializer.Serialize(a)); });
    }

    async Task<Address?> GetAddress(int id)
    {
        var address = await dbContext.Addresses.FindAsync(id);
        return address;
    }

    List<Address> GetAddresses()
    {
        var resut = dbContext.Addresses.ToList();
        return resut;
    }

    List<Student> GetStudents()
    {
        var studentsRes = dbContext.Students.ToList();
        return studentsRes;
    }

    async Task UpdateAddress(int id)
    {
        var address = await dbContext.Addresses.FindAsync(id);
        if (address != null)
        {
            address.City = GetRdmString(5);
            address.Zip = GetRdmNumericString(5);
            await dbContext.SaveChangesAsync();
        }
    }

// Careful with the dbContext instances which are used
    async Task UpdateAddressNotWorking(int id)
    {
        var addr = await GetAddress(id);

        if (addr != null)
        {
            addr.City = GetRdmString(5);
            addr.Zip = GetRdmNumericString(5);
            await dbContext.SaveChangesAsync();
        }
    }

    async Task DeleteAddress(int id)
    {
        var address = await dbContext.Addresses.FindAsync(id);
        if (address != null)
        {
            dbContext.Addresses.Remove(address);
            await dbContext.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("shutting down ...");
        return Task.CompletedTask;
    }
}
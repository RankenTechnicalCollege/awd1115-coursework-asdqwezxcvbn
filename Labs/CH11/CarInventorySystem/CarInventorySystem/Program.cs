using CarInventorySystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders.Physical;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarDb>(options => options.UseInMemoryDatabase("CarList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//http://localhost:5000/getCars

var app = builder.Build();

RouteGroupBuilder cars = app.MapGroup("/cars");

cars.MapGet("/", GetAllCars);
cars.MapGet("/available", GetAvailableCars);
cars.MapPost("/", CreateCar);
cars.MapPut("/{id}", UpdateCar);
cars.MapGet("/{id}", GetCar);
cars.MapDelete("/{id}", DeleteCar);

app.Run();

static async Task<IResult> GetAllCars(CarDb db)
{
    return TypedResults.Ok(await db.Cars.Select(x => new CarDTO(x)).ToArrayAsync());
}

static async Task<IResult> CreateCar(Car car, CarDb db)
{
    db.Cars.Add(car);
    await db.SaveChangesAsync();

    var dto = new CarDTO(car);

    return TypedResults.Created($"/cars/{car.Id}", dto);
}

static async Task<IResult> GetCar(int id, CarDb db)
{
    return await db.Cars.FindAsync(id)
        is Car car
            ? TypedResults.Ok(new CarDTO(car))
            : TypedResults.NotFound();
}

static async Task<IResult> DeleteCar(int id, CarDb db)
{
    if (await db.Cars.FindAsync(id) is Car car)
    {
        db.Cars.Remove(car);
        await db.SaveChangesAsync();
        return TypedResults.Ok(new CarDTO(car));
    }
    return TypedResults.NotFound();
}

static async Task<IResult> UpdateCar(int id, CarDTO carDTO, CarDb db)
{
    var car = await db.Cars.FindAsync(id);

    if (car is null) return TypedResults.NotFound();

    car.Make = carDTO.Make;
    car.Model = carDTO.Model;
    car.IsAvailable = carDTO.IsAvailable;

    await db.SaveChangesAsync();

    return TypedResults.Ok(new CarDTO(car));
}

static async Task<IResult> GetAvailableCars(CarDb db)
{
    return TypedResults.Ok(await db.Cars
        .Where(car => car.IsAvailable)
        .Select(x => new CarDTO(x))
        .ToArrayAsync());   
}
using System;
using System.Collections.Generic;

// Абстрактный класс для представления гостиничных номеров
public abstract class HotelRoom
{
    public abstract void DisplayInfo();
    public abstract decimal GetPrice();
}

// Класс для стандартного номера
public class StandardRoom : HotelRoom
{
    public override void DisplayInfo()
    {
        Console.WriteLine("Тип номера: Стандартный");
        Console.WriteLine("Цена: 1500");
        Console.WriteLine("Описание: Просторный номер с одной кроватью");
    }

    public override decimal GetPrice()
    {
        return 1500;
    }
}

// Класс для номера люкс
public class LuxuryRoom : HotelRoom
{
    public override void DisplayInfo()
    {
        Console.WriteLine("Тип номера: Люкс");
        Console.WriteLine("Цена: 3500");
        Console.WriteLine("Описание: Роскошный номер с двуспальной кроватью, мини-баром и видом на море");
    }

    public override decimal GetPrice()
    {
        return 3500;
    }
}

// Класс для апартаментов
public class Apartment : HotelRoom
{
    public override void DisplayInfo()
    {
        Console.WriteLine("Тип номера: Апартаменты");
        Console.WriteLine("Цена: 5000");
        Console.WriteLine("Описание: Просторные апартаменты с отдельной спальней, гостиной и кухней");
    }

    public override decimal GetPrice()
    {
        return 5000;
    }
}

// Фабрика для создания гостиничных номеров
public static class RoomFactory
{
    public static HotelRoom CreateRoom(string city, string roomType)
    {
        switch (roomType)
        {
            case "Стандартный":
                return new StandardRoom();
            case "Люкс":
                return new LuxuryRoom();
            case "Апартаменты":
                return new Apartment();
            default:
                throw new ArgumentException("Некорректный тип номера");
        }
    }
}

// Класс для системы бронирования гостиничных номеров
public class HotelBookingSystem
{
    private Dictionary<int, HotelRoom> bookedRooms;

    public Dictionary<int, HotelRoom> BookedRooms
    {
        get { return bookedRooms; }
    }

    public HotelBookingSystem()
    {
        bookedRooms = new Dictionary<int, HotelRoom>();
    }

    public int BookRoom(string city, string roomType, DateTime startDate, DateTime endDate)
    {
        HotelRoom room = RoomFactory.CreateRoom(city, roomType);
        int bookingId = GenerateBookingId();
        bookedRooms.Add(bookingId, room);
        Console.WriteLine("Номер успешно забронирован. ID бронирования: " + bookingId);

        decimal totalPrice = room.GetPrice() * (decimal)(endDate - startDate).TotalDays;
        Console.WriteLine("Итоговая стоимость: ₽" + totalPrice);

        return bookingId;
    }

    public void CancelBooking(int bookingId)
    {
        if (bookedRooms.ContainsKey(bookingId))
        {
            bookedRooms.Remove(bookingId);
            Console.WriteLine("Бронирование успешно отменено.");
        }
        else
        {
            Console.WriteLine("Бронирование с указанным ID не найдено.");
        }
    }
    private int GenerateBookingId()
    {
        // Генерация случайного ID бронирования
        Random random = new Random();
        return random.Next(1000, 9999);
    }

    static void Main(string[] args)
    {
        HotelBookingSystem bookingSystem = new HotelBookingSystem();

        // Выбор города
        Console.WriteLine("Доступные города: Сочи, Новороссийск, Анапа");
        Console.Write("Введите название города: ");
        string city = Console.ReadLine();

        //Меню
        Console.WriteLine("\n\t\tМеню");
        Console.WriteLine("Стандартный номер: для одного человека");
        Console.WriteLine("Люкс: Вид на море");
        Console.WriteLine("Апартаменты: Большие комнаты");


        // Поиск и бронирование номера
        Console.WriteLine("\nВыберите: Стандартный, Люкс, Апартаменты");
        Console.Write("Введите тип номера, который вы ищете: ");
        string roomType = Console.ReadLine();

        Console.WriteLine("\nВведите дату начала бронирования (yyyy-MM-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Введите дату окончания бронирования (yyyy-MM-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine());

        int bookingId = bookingSystem.BookRoom(city, roomType, startDate, endDate);

        Console.WriteLine();

        // Вывод информации о забронированном номере
        if (bookingSystem.BookedRooms.ContainsKey(bookingId))
        {
            HotelRoom bookedRoom = bookingSystem.BookedRooms[bookingId];
            Console.WriteLine("Информация о забронированном номере:");
            bookedRoom.DisplayInfo();
        }

        Console.WriteLine();

        // Отмена бронирования
        Console.Write("Хотите отменить бронирование? (да/нет): ");
        string cancelBooking = Console.ReadLine();

        if (cancelBooking.ToLower() == "да")
        {
            Console.Write("Введите ID бронирования: ");
            int cancelBookingId = int.Parse(Console.ReadLine());
            bookingSystem.CancelBooking(cancelBookingId);
        }
        if (cancelBooking.ToLower() == "нет")
        {
            Console.Write("Ждем вас в забронированном номере! Приятного отдыха!");
            int cancelBookingId = int.Parse(Console.ReadLine());

        }

        Console.ReadKey();
    }


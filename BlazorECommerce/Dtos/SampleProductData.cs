namespace BlazorECommerce.Dtos;
public static class SampleProductData
{
    public static List<ProductDto> GetAll() => new()
    {
        new() { Id = 1, Name = "Wireless Earbuds", Price = 1999, Description = "Compact earbuds with noise isolation and deep bass.", ImageUrl = "https://images.unsplash.com/photo-1585386959984-a4155224a1e1" },
        new() { Id = 2, Name = "Smart Bulb", Price = 799, Description = "16M colors, Wi-Fi enabled bulb with voice assistant support.", ImageUrl = "https://images.unsplash.com/photo-1594285800612-c152ab05a708" },
        new() { Id = 3, Name = "Portable Charger", Price = 1299, Description = "10,000mAh power bank with fast charging and Type-C support.", ImageUrl = "https://images.unsplash.com/photo-1606041008023-472dfb5e5304" },
        new() { Id = 4, Name = "Bluetooth Speaker", Price = 1499, Description = "Waterproof speaker with 10hr battery life and stereo sound.", ImageUrl = "https://images.unsplash.com/photo-1611175694984-c09a70b263d4" },
        new() { Id = 5, Name = "Fitness Tracker", Price = 2499, Description = "Track steps, heart rate, sleep and more with this wearable.", ImageUrl = "https://images.unsplash.com/photo-1586864381218-d2a5d84d4797" },
        new() { Id = 6, Name = "Webcam 1080p", Price = 999, Description = "Crystal-clear video calling with built-in mic and tripod.", ImageUrl = "https://images.unsplash.com/photo-1602524203493-1798f6b1db3c" },
        new() { Id = 7, Name = "Wireless Mouse", Price = 699, Description = "Ergonomic design with precision tracking and silent clicks.", ImageUrl = "https://images.unsplash.com/photo-1573497491208-6b1acb260507" },
        new() { Id = 8, Name = "Mechanical Keyboard", Price = 2999, Description = "RGB backlight and tactile switches for better typing.", ImageUrl = "https://images.unsplash.com/photo-1555617117-08fda0ed3d8a" },
        new() { Id = 9, Name = "USB-C Hub", Price = 1199, Description = "6-in-1 hub with HDMI, USB 3.0, SD card reader & PD charging.", ImageUrl = "https://images.unsplash.com/photo-1610057098252-081e2531218a" },
        new() { Id = 10, Name = "LED Desk Lamp", Price = 899, Description = "Touch control with adjustable brightness and color temp.", ImageUrl = "https://images.unsplash.com/photo-1578973556813-0c7c13a0e5c5" }
    };
}

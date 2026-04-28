using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TainanBackend.Models;

namespace TainanBackend.Controllers;

// [ApiController] -> 告訴 .NET 這是一個 API 控制器
// [Route("api/[controller]")] -> 路由規則，[controller] 會自動取代成類別名稱（去掉 Controller）
// 所以這個類別叫 RestaurantsController，路由就是 /api/restaurants
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    // 資料庫連線物件，用來操作資料庫
    private readonly AppDbContext _context;

    // 建構子：.NET 會自動把 AppDbContext 傳進來（依賴注入）
    public RestaurantsController(AppDbContext context)
    {
        _context = context;
    }

    // [HttpGet] -> 處理 GET 請求
    // 前端呼叫 GET /api/restaurants 時，會執行這個方法
    // async -> 非同步，避免阻塞執行緒
    // Task<ActionResult<List<Restaurant>>> -> 回傳型別，表示回傳 Restaurant 的 List
    [HttpGet]
    public async Task<ActionResult<List<Restaurant>>> GetAll()
    {
        // ToListAsync() -> EF Core 產生的 SQL：SELECT * FROM restaurants
        var restaurants = await _context.Restaurants.ToListAsync();
        return Ok(restaurants); // Ok() -> HTTP 200，把資料回傳給前端
    }


    [HttpGet("random")]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRandom([FromQuery] int count = 1)
    {
        // 1. 取得資料庫總筆數
        var totalCount = await _context.Restaurants.CountAsync();

        // 2. 防呆機制：如果資料庫是空的
        if (totalCount == 0) return NotFound("資料庫內沒有餐廳。");

        // 3. 防呆機制：請求數量不能超過總數，也不能小於 1
        int limit = Math.Min(count, totalCount);
        if (limit <= 0) limit = 1;

        // 4. 隨機排序並取得指定數量
        var restaurants = await _context.Restaurants
            .OrderBy(r => Guid.NewGuid())
            .Take(limit)
            .ToListAsync();

        return Ok(restaurants);
    }

    // [HttpGet("{id}")] -> 處理 GET /api/restaurants/5
    // {id} 是路徑參數，會自動對應到方法參數 int id
    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetById(int id)
    {
        // FindAsync(id) -> SQL：SELECT * FROM restaurants WHERE id = 5
        var restaurant = await _context.Restaurants.FindAsync(id);
        
        if (restaurant == null)
            return NotFound(); // HTTP 404
            
        return Ok(restaurant); // HTTP 200
    }
}
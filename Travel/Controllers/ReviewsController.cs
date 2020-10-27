using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private TravelContext _db;

    public ReviewsController(TravelContext db)
    {
      _db = db;
    }

    // GET api/reviews/5
    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      return _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
    }

    // GET api/reviews
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get(string city, string country)
    {
      var query = _db.Reviews.AsQueryable();

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }
      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }

      List<Review> reviews = _db.Reviews.ToList();
      var mostReviews = 
        from review in reviews
        group review by review.City into reviewGroup
        select new
        {
          city = reviewGroup.Key,
          ReviewCount = reviewGroup.Count(),
        };
      var chair = mostReviews;
      return Ok(mostReviews);
        

      // return query.ToList();
    }

    //   // GET api/reviews
    // [HttpGet]
    // public ActionResult<IEnumerable<Review>> Get(string city, string country, int rating)
    // {
    //   var query = _db.Reviews.AsQueryable();

    //   if (city != null)
    //   {
    //     query = query.Where(entry => entry.City == city);
    //   }
    //   if (country != null)
    //   {
    //     query = query.Where(entry => entry.Country == country);
    //   }
    //   if (rating != 0)
    //   {
    //     query = query.Where(entry => entry.Rating == rating);
    //   }
    
      
    //   return query.ToList();
    // }

    // POST api/reviews
    [HttpPost]
    public void Post([FromBody] Review review)
    {
      _db.Reviews.Add(review);
      _db.SaveChanges();
    }

    // PUT api/reviews/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Review review)
    {
      review.ReviewId = id;
      _db.Entry(review).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/reviews/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var reviewToDelete = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
      _db.Reviews.Remove(reviewToDelete);
      _db.SaveChanges();
    }
  }
}
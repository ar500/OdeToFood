using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using Remotion.Linq.Clauses;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _dbContext.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            if (restaurant != null)
            {
                _dbContext.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return _dbContext.Restaurants.Count();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _dbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            var query = from r in _dbContext.Restaurants
                where r.Name.StartsWith(name) || string.IsNullOrWhiteSpace(name)
                orderby r.Name
                select r;

            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _dbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFoot.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Indian's pizza", Location = "Maryland", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 2, Name = "Mexican's pizza", Location = "Maryland2", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 3, Name = "Scott's pizza", Location = "Maryland3", Cuisine = CuisineType.None },
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0; 
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            //return from r in restaurants
            //       where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
            //       orderby r.Name
            //       select r;
            if (string.IsNullOrEmpty(name))
                return restaurants.OrderBy(r => r.Name);

            return restaurants.Where(r => r.Name.ToLower().Contains(name.ToLower()))
                              .OrderBy(r => r.Name);          

        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}

using CityInformation.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CityInformation.Database
{
    public static class CityInformationDefaultData
    {
        public static void DataSeed(this CityInformationContext cityInformationContext)
        {
            if (cityInformationContext.Cities.Any())
            {
                return;
            }

            var cities = new List<CityEntity>
            {
                new CityEntity
                {
                    Name = "Amsterdam",
                    Description = "Amsterdam is the Netherlands’ capital, known for its artistic heritage.",
                    
                    PointOfInterest = new List<PointOfInterestEntity>
                    {
                        new PointOfInterestEntity
                        {
                            Name = "The Rijksmuseum",
                            Description = "Rijksmuseum was founded in 1809 to house the country's huge collection of rare art and antiquities."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "The Anne Frank Museum",
                            Description = "Anne Frank Museum is dedicated to the all-too-short life of one of the world's best-known Holocaust victims."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "The West Church",
                            Description = "Amsterdam's West Church (Westerkerk), famous as the location of the wedding of former Queen Beatrix in 1966."
                        }
                    }
                },

                new CityEntity
                {
                    Name = "Antwerp",
                    Description = "Antwerp is a port city on Belgium’s River Scheldt, with history dating to the Middle Ages.",

                    PointOfInterest = new List<PointOfInterestEntity>
                    {
                        new PointOfInterestEntity
                        {
                            Name = "Admire Antwerp Central Station",
                            Description = "Looking at a train station may not be one of the best things to do in many cities but in Antwerp, it is an absolute must."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "The Museum Plantin-Moretus",
                            Description = "This former printing press was once one of the finest in the world. The site has been home to museum since 1876 and is now a UNSECO World Heritage Site."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "Pop into Ruben’s House",
                            Description = "Rubenshuis is the former home of the painter Pieter Paul Rubens. The home was built by Rubens himself."
                        }
                    }
                },

                new CityEntity
                {
                    Name = "New York",
                    Description = "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean.",
                    PointOfInterest = new List<PointOfInterestEntity>
                    {
                        new PointOfInterestEntity
                        {
                            Name = "Statue of Liberty",
                            Description = "The Statue of Liberty was France's gift to America. Built in 1886, it remains a famous world symbol of freedom and one of the greatest American icons."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "Central Park",
                            Description = "A walk, peddle, or carriage ride through the crisscrossing pathways of Central Park is a must-do on anyone's New York City itinerary."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "Metropolitan Museum of Art",
                            Description = "The Metropolitan Museum of Art, or the Met, as it is commonly known, was founded in 1870, and is one of the most famous museums in the United States."
                        },
                        new PointOfInterestEntity
                        {
                            Name = "Empire State Building",
                            Description = "The Empire State Building is one of New York's most famous landmark buildings. This 381-meter-tall and 102-storey building opened in 1931."
                        }
                    }
                }
            };

            cityInformationContext.Cities.AddRange(cities);
            cityInformationContext.SaveChanges();
        }
    }
}

using CityInformation.Api.Models;
using System.Collections.Generic;

namespace CityInformation.Api
{
    public class CitiesDataStore
    {
        public static CitiesDataStore CurrentDataStore { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    CityId = 1,
                    Name = "Amsterdam",
                    Description = "Amsterdam is the Netherlands’ capital, known for its artistic heritage.",
                    PointsOfInterest = new List<NumberOfPointsOfInterestDto>
                    {
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 1,
                            Name = "The Rijksmuseum",
                            Description = "Rijksmuseum was founded in 1809 to house the country's huge collection of rare art and antiquities. The museum's impressive collection includes some seven million works of art, among them more than 5,000 important paintings."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 2,
                            Name = "The Anne Frank Museum",
                            Description = "Anne Frank Museum is dedicated to the all-too-short life of one of the world's best-known Holocaust victims."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 3,
                            Name = "The West Church",
                            Description = "Amsterdam's West Church (Westerkerk), famous as the location of the wedding of former Queen Beatrix in 1966, is the most popular church in the city."
                        }
                    }
                },

                new CityDto
                {
                    CityId = 2,
                    Name = "Antwerp",
                    Description = "Antwerp is a port city on Belgium’s River Scheldt, with history dating to the Middle Ages.",
                    PointsOfInterest = new List<NumberOfPointsOfInterestDto>
                    {
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 4,
                            Name = "Admire Antwerp Central Station",
                            Description = "Looking at a train station may not be one of the best things to do in many cities but in Antwerp, it is an absolute must. The Gothic architecture of the building’s impressive exterior will take your breath away and the main hall is equally splendid."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 5,
                            Name = "The Museum Plantin-Moretus",
                            Description = "This former printing press was once one of the finest in the world. The site has been home to museum since 1876 and is now a UNSECO World Heritage Site."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 6,
                            Name = "Pop into Ruben’s House",
                            Description = "Rubenshuis is the former home of the painter Pieter Paul Rubens. The home was built by Rubens himself as both a place to live and also as a studio for him to work in."
                        }
                    }
                },

                new CityDto
                {
                    CityId = 3,
                    Name = "New York",
                    Description = "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean.",
                    PointsOfInterest = new List<NumberOfPointsOfInterestDto>
                    {
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 7,
                            Name = "Statue of Liberty",
                            Description = "The Statue of Liberty was France's gift to America. Built in 1886, it remains a famous world symbol of freedom and one of the greatest American icons."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 8,
                            Name = "Central Park",
                            Description = "A walk, peddle, or carriage ride through the crisscrossing pathways of Central Park is a must-do on anyone's New York City itinerary. In winter, you can even lace up your skates and glide across Wollman Rink. This huge park in the city center, a half-mile wide and 2.5 miles long, is one of the things that makes New York such a beautiful and livable city. Besides being a great place to experience a little nature, Central Park has many attractions within its borders, including the Belvedere Castle, Strawberry Fields, the Central Park Zoo, and the Lake."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 9,
                            Name = "Metropolitan Museum of Art",
                            Description = "The Metropolitan Museum of Art, or the Met, as it is commonly known, was founded in 1870, and is one of the most famous museums in the United States. The permanent collection of The Met contains more than two million works of art, spanning a period of 5,000 years. Although the museum has three sites, the centerpiece is The Met Fifth Avenue."
                        },
                        new NumberOfPointsOfInterestDto
                        {
                            PointsOfInterestId = 10,
                            Name = "Empire State Building",
                            Description = "The Empire State Building is one of New York's most famous landmark buildings. The 381-meter-tall, 102-storey building was the tallest in the world until the 1 World Trade Center tower rose higher, 41 years later. Topped with a mooring mast for airships, the Empire State Building immediately became a landmark and a symbol for NYC when it opened in 1931."
                        }
                    }
                }
            };
        }
    }
}

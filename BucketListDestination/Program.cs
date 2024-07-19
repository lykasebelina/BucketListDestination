using System;
using BucketListData;
using BucketListModels;
using BucketListServices;

namespace BucketListDestination
{
    class Program
    {
        static void Main(string[] args)
        {
            UserGetServices getServices = new UserGetServices();

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (getServices.ValidateUser(username, password))
            {
                Console.WriteLine("My Bucket List Destinations:");
                DestinationGetServices destinationGetServices = new DestinationGetServices();
                var destinations = destinationGetServices.GetAllDestinations();

                foreach (var destination in destinations)
                {
                    Console.WriteLine(destination.Name);
                }

                Console.Write("Do you want to add new destination? (Type Yes or No): ");
                string addChoice = Console.ReadLine();

                if (addChoice.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter Destination Name: ");
                    string newName = Console.ReadLine();

                    Console.Write("Enter Capital: ");
                    string newCapital = Console.ReadLine();

                    Console.Write("Enter Language: ");
                    string newLanguage = Console.ReadLine();

                    Console.Write("Enter Currency: ");
                    string newCurrency = Console.ReadLine();

                    Console.Write("Enter Citizenship: ");
                    string newCitizenship = Console.ReadLine();

                    Console.Write("Enter DONE to save new bucket list record: ");
                    string doneInput = Console.ReadLine();

                    if (doneInput.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                    {
                        // Save the new destination record to the database
                        Destination newDestination = new Destination
                        {
                            Name = newName,
                            Capital = newCapital,
                            Language = newLanguage,
                            Currency = newCurrency,
                            Citizenship = newCitizenship
                        };

                        destinationGetServices.AddNewDestination(newDestination);

                        Console.WriteLine("New destination added successfully.");

                        // Refresh destinations list after adding new record
                        destinations = destinationGetServices.GetAllDestinations();
                        Console.WriteLine("\nUpdated Bucket List Destinations:");
                        foreach (var destination in destinations)
                        {
                            Console.WriteLine(destination.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Process Cancelled. No changes made.");
                    }
                }
                else if (addChoice.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Process Done. No changes made.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Process Cancelled. No changes made.");
                }

                // Prompt for deleting a destination
                Console.Write("Do you want to delete a destination? (Type Yes or No): ");
                string deleteChoice = Console.ReadLine();

                if (deleteChoice.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter destination name to delete: ");
                    string deleteName = Console.ReadLine();

                    // Check if the destination exists before attempting to delete
                    var destinationToDelete = destinationGetServices.GetDestinationByName(deleteName);
                    if (destinationToDelete != null)
                    {
                        destinationGetServices.DeleteDestination(deleteName);
                        Console.WriteLine($"Destination '{deleteName}' deleted successfully.");

                        // Refresh destinations list after deletion
                        destinations = destinationGetServices.GetAllDestinations();
                        Console.WriteLine("\nUpdated Bucket List Destinations:");
                        foreach (var destination in destinations)
                        {
                            Console.WriteLine(destination.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Destination '{deleteName}' not found. No changes made.");
                    }
                }
                else if (deleteChoice.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Process Done. No changes made.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Process Cancelled. No changes made.");
                }

                // Prompt for viewing a destination
                Console.Write("Do you want to view destination info? (Type Yes or No): ");
                string viewChoice = Console.ReadLine();

                if (viewChoice.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter a destination: ");
                    string viewName = Console.ReadLine();

                    var destinationToView = destinationGetServices.GetDestinationByName(viewName);
                    if (destinationToView != null)
                    {
                        Console.WriteLine($"Name: {destinationToView.Name}");
                        Console.WriteLine($"Capital: {destinationToView.Capital}");
                        Console.WriteLine($"Language: {destinationToView.Language}");
                        Console.WriteLine($"Currency: {destinationToView.Currency}");
                        Console.WriteLine($"Citizenship: {destinationToView.Citizenship}");
                    }
                    else
                    {
                        Console.WriteLine($"Destination '{viewName}' not found.");
                    }
                }
                else if (viewChoice.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Process Done. No changes made.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Process Cancelled. No changes made.");
                }

                // Prompt for updating a destination
                Console.Write("Do you want to update a destination? (Type Yes or No): ");
                string updateChoice = Console.ReadLine();

                if (updateChoice.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter a destination: ");
                    string updateName = Console.ReadLine();

                    var destinationToUpdate = destinationGetServices.GetDestinationByName(updateName);
                    if (destinationToUpdate != null)
                    {
                        Console.Write($"Enter new Capital (current: {destinationToUpdate.Capital}): ");
                        string newCapital = Console.ReadLine();

                        Console.Write($"Enter new Language (current: {destinationToUpdate.Language}): ");
                        string newLanguage = Console.ReadLine();

                        Console.Write($"Enter new Currency (current: {destinationToUpdate.Currency}): ");
                        string newCurrency = Console.ReadLine();

                        Console.Write($"Enter new Citizenship (current: {destinationToUpdate.Citizenship}): ");
                        string newCitizenship = Console.ReadLine();

                        Console.Write("Enter DONE to save changes: ");
                        string doneUpdate = Console.ReadLine();

                        if (doneUpdate.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                        {
                            // Update the destination record in the database
                            Destination updatedDestination = new Destination
                            {
                                Name = updateName,
                                Capital = newCapital,
                                Language = newLanguage,
                                Currency = newCurrency,
                                Citizenship = newCitizenship
                            };

                            destinationGetServices.UpdateDestination(updatedDestination);

                            Console.WriteLine("Destination updated successfully.");

                            // Display updated destination details
                            var updatedDestinationInfo = destinationGetServices.GetDestinationByName(updateName);
                            if (updatedDestinationInfo != null)
                            {
                                Console.WriteLine("\nUpdated Destination Info:");
                                Console.WriteLine($"Name: {updatedDestinationInfo.Name}");
                                Console.WriteLine($"Capital: {updatedDestinationInfo.Capital}");
                                Console.WriteLine($"Language: {updatedDestinationInfo.Language}");
                                Console.WriteLine($"Currency: {updatedDestinationInfo.Currency}");
                                Console.WriteLine($"Citizenship: {updatedDestinationInfo.Citizenship}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Process Cancelled. No changes made.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Destination '{updateName}' not found. No changes made.");
                    }
                }
                else if (updateChoice.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Process Done. No changes made.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Process Cancelled. No changes made.");
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
    }
}

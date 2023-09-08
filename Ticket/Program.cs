using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "new_tickets.csv";

        // Create the header if the file doesn't exist
        if (!File.Exists(filePath))
        {
            CreateHeader(filePath);
        }

        List<string[]> data = ReadDataFromFile(filePath);

        // Ask the user for input for each field
        Console.WriteLine("Enter TicketID:");
        string ticketID = Console.ReadLine();

        Console.WriteLine("Enter Summary:");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter Status:");
        string status = Console.ReadLine();

        Console.WriteLine("Enter Priority:");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter Submitter:");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter Assigned:");
        string assigned = Console.ReadLine();

        string watchingNames = GetUserInputForWatching();

        // Create a new ticket entry
        string[] newTicket = { ticketID, summary, status, priority, submitter, assigned, watchingNames };
        data.Add(newTicket);

        // Write the updated data to the file, overwriting the existing content
        WriteDataToFile(filePath, data);

        Console.WriteLine("Data written to the file.");
    }

    static List<string[]> ReadDataFromFile(string filePath)
    {
        List<string[]> data = new List<string[]>();
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the header line
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    data.Add(values);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
        return data;
    }

    static void WriteDataToFile(string filePath, List<string[]> data)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header
                writer.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

                foreach (string[] row in data)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to file: " + ex.Message);
        }
    }

    static void CreateHeader(string filePath)
    {
        string header = "TicketID,Summary,Status,Priority,Submitter,Assigned,Watching";
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(header);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating header: " + ex.Message);
        }
    }

    static string GetUserInputForWatching()
    {
        Console.WriteLine("Enter the Watching names separated by '|'. Enter 'done' when finished:");
        List<string> watchingNamesList = new List<string>();

        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "done")
            {
                break;
            }
            watchingNamesList.Add(input);
        }

        string watchingNames = string.Join("|", watchingNamesList);
        return watchingNames;
    }
}


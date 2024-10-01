using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static Semaphore semaphore;
    private static List<string> createdThreads = new List<string>();
    private static List<string> waitingThreads = new List<string>();
    private static int semaphoreCount = 1;

    static void Main(string[] args)
    {
        semaphore = new Semaphore(semaphoreCount, 5);
        string command;

        do
        {
            Console.Clear();
            DisplayStatus();

            Console.WriteLine("Commands:");
            Console.WriteLine("1. Create Thread");
            Console.WriteLine("2. Select Thread (created)");
            Console.WriteLine("3. Select Thread (waiting)");
            Console.WriteLine("4. Increase Semaphore");
            Console.WriteLine("5. Decrease Semaphore");
            Console.WriteLine("6. Exit");
            Console.Write("Enter command: ");
            command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    CreateThread();
                    break;
                case "2":
                    SelectCreatedThread();
                    break;
                case "3":
                    SelectWaitingThread();
                    break;
                case "4":
                    IncreaseSemaphore();
                    break;
                case "5":
                    DecreaseSemaphore();
                    break;
            }
        } while (command != "6");
    }

    private static void DisplayStatus()
    {
        Console.WriteLine("Created Threads:");
        for (int i = 0; i < createdThreads.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {createdThreads[i]}");
        }

        Console.WriteLine("\nWaiting Threads:");
        for (int i = 0; i < waitingThreads.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {waitingThreads[i]}");
        }

        Console.WriteLine($"\nSemaphore Count: {semaphoreCount}");
    }

    private static void CreateThread()
    {
        var threadNumber = createdThreads.Count + 1;
        createdThreads.Add($"Thread {threadNumber}");
        Console.WriteLine($"Created Thread {threadNumber}");
        Thread.Sleep(1000);
    }

    private static void SelectCreatedThread()
    {
        Console.Write("Select thread number to wait: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= createdThreads.Count)
        {
            var selectedThread = createdThreads[index - 1];
            waitingThreads.Add(selectedThread);
            createdThreads.RemoveAt(index - 1);
            Console.WriteLine($"Moved {selectedThread} to waiting.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
        Thread.Sleep(1000);
    }

    private static void SelectWaitingThread()
    {
        Console.Write("Select thread number to release: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= waitingThreads.Count)
        {
            var selectedThread = waitingThreads[index - 1];
            waitingThreads.RemoveAt(index - 1);
            ReleaseSemaphore();
            Console.WriteLine($"Released {selectedThread} from waiting.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
        Thread.Sleep(1000);
    }

    private static void ReleaseSemaphore()
    {
        if (semaphoreCount < 5)
        {
            semaphore.Release();
            semaphoreCount++;
            Console.WriteLine("Semaphore released.");
        }
    }

    private static void IncreaseSemaphore()
    {
        if (semaphoreCount < 5)
        {
            semaphoreCount++;
            Console.WriteLine("Increased semaphore count.");
        }
        else
        {
            Console.WriteLine("Maximum semaphore count reached.");
        }
    }

    private static void DecreaseSemaphore()
    {
        if (semaphoreCount > 1)
        {
            semaphoreCount--;
            Console.WriteLine("Decreased semaphore count.");
        }
        else
        {
            Console.WriteLine("Minimum semaphore count reached.");
        }
    }
}

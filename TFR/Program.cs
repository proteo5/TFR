using Cocona;

namespace TFR;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();
        var app = builder.Build();

        app.AddCommand((
            [Argument(Description = "Specify a paths file")]
            string? pathFile,
            [Option('s', Description = "Skip delete confirmation")]
            bool skipConfirmation
            ) =>
        {
            #region Validations

            //check if the string pathsFilepathArg is empty, otherwise use the default value
            string pathsFilepath = string.IsNullOrEmpty(pathFile) ?
                            pathsFilepath = Path.Combine(Directory.GetCurrentDirectory(), "paths.txt") :
                            pathFile;

            Console.WriteLine($"Paths file: {pathsFilepath}");
            Console.WriteLine();

            //check if the string files has less than 2000 characters
            if (pathsFilepath.Length > 2000)
            {
                Console.WriteLine($"The maximum number of characters for a paths file is 2000 ({pathsFilepath.Length})");
                return;
            }

            //check if the file exists
            if (!File.Exists(pathsFilepath))
            {
                Console.WriteLine($"The file {pathsFilepath} does not exist");
                return;
            }

            //check if the file is empty
            if (new FileInfo(pathsFilepath).Length == 0)
            {
                Console.WriteLine($"The file {pathsFilepath} is empty");
                return;
            }

            //check if each line in the file is a valid path
            var paths = File.ReadAllLines(pathsFilepath);
            bool foundInvalidPath = false;
            foreach (var path in paths)
            {
                //if the path is empty or if starts with #, skip it
                if (string.IsNullOrEmpty(path) || path.StartsWith("#"))
                {
                    continue;
                }

                if (!Directory.Exists(path))
                {
                    foundInvalidPath = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Path invalid: {path}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Path valid: {path}");
                    Console.ResetColor();
                }
            }

            if (foundInvalidPath)
            {
                Console.WriteLine();
                Console.WriteLine("Please fix the invalid paths and try again");
                return;
            }

            #endregion

            //confirm if the user wants to delete the files 
            if (!skipConfirmation)
            {
                Console.WriteLine();
                Console.WriteLine($"Do you want to delete the files and folders on the paths ? (Y/N)");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Operation canceled by user");
                    return;
                }
            }

            //delete the files and folders on the paths
            foreach (var path in paths)
            {
                //if the path is empty or if starts with #, skip it
                if (string.IsNullOrEmpty(path) || path.StartsWith("#"))
                {
                    continue;
                }

                Console.WriteLine($"Deleting: {path}");
                //Directory.Delete(path, true);
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (!IsFileLocked(file))
                    {
                        file.Delete();
                    }
                    else
                    {
                        Console.WriteLine($"The file {file.FullName} is locked and can't be deleted.");
                    }
                }

                foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
                {
                    if (!IsDirectoryLocked(subDirectory))
                    {
                        subDirectory.Delete(true);
                    }
                    else
                    {
                        Console.WriteLine($"The folder {subDirectory.FullName} is locked and can't be deleted.");
                    }
                }

            }

            //inidicate that the operation was successful and the number of paths deleted
            Console.WriteLine();
            Console.WriteLine("Operation completed successfully");
            Console.WriteLine($"Number of paths deleted: {paths.Length}");
            Console.WriteLine();

        });

        app.Run();
    }

    static bool IsFileLocked(FileInfo file)
    {
        try
        {
            using (FileStream stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                stream.Close();
            }
        }
        catch (IOException)
        {
            return true;
        }
        return false;
    }

    static bool IsDirectoryLocked(DirectoryInfo directory)
    {
        try
        {
            // Intenta abrir un archivo en el directorio para verificar si el directorio está en uso
            string tempFilePath = Path.Combine(directory.FullName, Path.GetRandomFileName());
            using (FileStream stream = new FileStream(tempFilePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None))
            {
                stream.Close();
            }
            File.Delete(tempFilePath);
        }
        catch (IOException)
        {
            return true;
        }
        return false;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace UsageCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedList lstÉtudiant = new SortedList();

            Console.Write("Entrez le nombre d'étudiants : ");
            int nombreÉtudiants = int.Parse(Console.ReadLine());

            for (int i = 0; i < nombreÉtudiants; i++)
            {
                Console.WriteLine($"\nSaisie de l'étudiant {i + 1}:");
                Console.Write("NO : ");
                string no = Console.ReadLine();

                Console.Write("Prénom : ");
                string prénom = Console.ReadLine();

                Console.Write("Nom : ");
                string nom = Console.ReadLine();

                Console.Write("NoteCC : ");
                int noteCC = int.Parse(Console.ReadLine());

                Console.Write("NoteDevoir : ");
                int noteDevoir = int.Parse(Console.ReadLine());

                Étudiant étudiant = new Étudiant
                {
                    NO = no,
                    Prénom = prénom,
                    Nom = nom,
                    NoteCC = noteCC,
                    NoteDevoir = noteDevoir
                };

                lstÉtudiant.Add(no, étudiant);
            }

            List<Étudiant> étudiants = new List<Étudiant>();
            List<double> moyennes = new List<double>();
            double totalMoyenne = 0;

            foreach (DictionaryEntry entry in lstÉtudiant)
            {
                Étudiant e = (Étudiant)entry.Value;
                double m = (e.NoteCC * 0.33) + (e.NoteDevoir * 0.67);
                étudiants.Add(e);
                moyennes.Add(m);
                totalMoyenne += m;
            }

            double moyenneClasse = étudiants.Count > 0 ? totalMoyenne / étudiants.Count : 0;

            int linesPerPage = 5;
            do
            {
                Console.Write("\nNombre de lignes par page [1-15] (défaut 5) : ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;
                if (!int.TryParse(input, out linesPerPage) || linesPerPage < 1 || linesPerPage > 15)
                {
                    Console.WriteLine("Valeur invalide !");
                    linesPerPage = 5;
                }
                else break;
            } while (true);

            int page = 0;
            bool exit = false;
            
            while (page * linesPerPage < étudiants.Count && !exit)
            {
                Console.Clear();
                Console.WriteLine($"=== Page {page + 1} ===\n");
                Console.WriteLine("NO".PadRight(10) + "Nom".PadRight(15) + "Prénom".PadRight(15) + 
                                "CC".PadRight(5) + "Devoir".PadRight(7) + "Moyenne");
                Console.WriteLine(new string('-', 60));

                for (int i = page * linesPerPage; i < (page + 1) * linesPerPage && i < étudiants.Count; i++)
                {
                    Étudiant e = étudiants[i];
                    Console.WriteLine(
                        $"{e.NO.PadRight(10)}{e.Nom.PadRight(15)}{e.Prénom.PadRight(15)}" +
                        $"{e.NoteCC.ToString().PadRight(5)}{e.NoteDevoir.ToString().PadRight(7)}{moyennes[i]:F2}"
                    );
                }

                page++;
                if (page * linesPerPage < étudiants.Count)
                {
                    Console.WriteLine("\nAppuyez sur :");
                    Console.WriteLine("[Entrée] - Page suivante");
                    Console.WriteLine("Q - Quitter");
                    var key = Console.ReadKey();
                    
                    if (key.KeyChar.ToString().ToUpper() == "Q")
                    {
                        exit = true;
                    }
                }
            }

            Console.WriteLine($"\nMoyenne générale de la classe : {moyenneClasse:F2}");
            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour quitter...");
            Console.ReadKey();
        }
    }
}
using System.Diagnostics.CodeAnalysis;

namespace Lab_2;

public abstract class Employer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }
    public bool HasHigherEducation { get; set; }

    public override string ToString()
    {
        return $"Ім'я: {FirstName}\n" +
               $"Прізвище: {LastName}\n" +
               $"Дата народження: {BirthDate:dd.MM.yyyy}\n" +
               $"Посада: {Position}\n" +
               $"Зарплата: {Salary:C}\n" +
               $"Дата початку роботи: {StartDate:dd.MM.yyyy}\n" +
               $"Вища освіта: {(HasHigherEducation ? "Так" : "Ні")}";
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;

namespace LosowanieOsoby
{
    public partial class MainPage : ContentPage
    {
        private List<string> students = new();
        private string classFilePath = "";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryStudentName.Text))
            {
                students.Add(entryStudentName.Text);
                listViewStudents.ItemsSource = null;
                listViewStudents.ItemsSource = students;
                entryStudentName.Text = "";
            }
        }

        private async void OnSaveListClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryClassName.Text))
            {
                await DisplayAlert("Błąd", "Podaj nazwę klasy!", "OK");
                return;
            }

            FileHelper.SaveListToFile(entryClassName.Text, students);
            await DisplayAlert("Sukces", $"Lista klasy {entryClassName.Text} zapisana!", "OK");
        }

        private async void OnLoadListClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryClassName.Text))
            {
                await DisplayAlert("Błąd", "Podaj nazwę klasy!", "OK");
                return;
            }

            students = FileHelper.LoadListFromFile(entryClassName.Text);
            listViewStudents.ItemsSource = null;
            listViewStudents.ItemsSource = students;

            await DisplayAlert("Informacja", $"Załadowano listę klasy {entryClassName.Text}.", "OK");
        }

        private async void OnDrawStudentClicked(object sender, EventArgs e)
        {
            if (students.Count == 0)
            {
                await DisplayAlert("Błąd", "Brak uczniów na liście!", "OK");
                return;
            }

            Random rand = new();
            int index = rand.Next(students.Count);
            labelSelectedStudent.Text = "Wylosowana osoba: " + students[index];
        }

        private async void OnRemoveStudentClicked(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItem != null)
            {
                string selectedStudent = listViewStudents.SelectedItem.ToString();
                students.Remove(selectedStudent);
                listViewStudents.ItemsSource = null;
                listViewStudents.ItemsSource = students;
            }
            else
            {
                await DisplayAlert("Błąd", "Nie zaznaczono ucznia do usunięcia!", "OK");
            }
        }
    }
}

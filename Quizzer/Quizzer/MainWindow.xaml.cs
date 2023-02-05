﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Quizzer;

public partial class MainWindow : Window
{
    List<Quiz> quizzes = Database.loadQuizzes();
    QuizPage QuizPage;
    const int MAXQUESTIONS = 10;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Quiz_BtnClick(object sender, RoutedEventArgs e)
    {
        // extract number from button content (Quiz * --> *) and load quiz
        string s = ((Button)sender).Content.ToString();
        LoadQuiz(int.Parse(s.Replace("Quiz", "")));
    }
    
    private void LoadQuiz(int quiz_num)
    {
        var quiz = quizzes[quiz_num];
        var all_questions = quiz.Questions.ToList();
        var questions = new List<Question>(MAXQUESTIONS);
        var rand = new Random(DateTime.Now.Millisecond);
        var total = all_questions.Count;
        int index;

        for (int i = 0; i < MAXQUESTIONS; i++)
        {
            index = rand.Next(total);
            questions.Add(all_questions[index]);
            all_questions.RemoveAt(index);
            total--;
        }

        QuizPage = new QuizPage(questions, _NavigationFrame);
        _NavigationFrame.Navigate(QuizPage);
    }
}

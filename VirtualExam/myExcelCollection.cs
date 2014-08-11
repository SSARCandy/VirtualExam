using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExam
{
    class myExcelCollection
    {
        private string question;
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private string answer;

        private bool showAnswer = false;
        private bool mark = false;

        private string usersAnswer = "";
        public myExcelCollection() { }
        public myExcelCollection(string question, string answerA, string answerB, string answerC, string answerD, string answer)
        {
            this.question = question;
            this.answerA = answerA;
            this.answerB = answerB;
            this.answerC = answerC;
            this.answerD = answerD;
            this.answer = answer;
            findAnswer();
        }
        void findAnswer()
        {
            if (this.answer == "A")
            {
                this.answer = this.answerA;
            }
            else if (this.answer == "B")
            {
                this.answer = this.answerB;
            }
            else if (this.answer == "C")
            {
                this.answer = this.answerC;
            }
            else if (this.answer == "D")
            {
                this.answer = this.answerD;
            }

        }
        public string getQuestion()
        {
            return this.question;
        }
        public string getAnswerA()
        {
            return this.answerA;
        }
        public string getAnswerB()
        {
            return this.answerB;
        }
        public string getAnswerC()
        {
            return this.answerC;
        }
        public string getAnswerD()
        {
            return this.answerD;
        }
        public string getAnswer()
        {
            return this.answer;
        }
        public string getUsersAnswer()
        {
            return this.usersAnswer;
        }
        public void setUsersAnswer(string usersAnswer)
        {
            this.usersAnswer = usersAnswer;
        }
        public void setMark(bool mark)
        {
            this.mark = mark;
        }
        public bool getMark()
        {
            return mark;
        }
        public bool getShowAnswer()
        {
            return showAnswer;
        }
        public void setShowAnswer(bool b)
        {
            showAnswer = b;
        }
        public void randOption()
        {
            string[] s = new string[4];
            s[0] = this.answerA;
            s[1] = this.answerB;
            s[2] = this.answerC;
            s[3] = this.answerD;
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                int r1 = r.Next(0, 3);
                string s1 = s[0]; s[0] = s[r1]; s[r1] = s1;
            }
            this.answerA = s[0];
            this.answerB = s[1];
            this.answerC = s[2];
            this.answerD = s[3];
        }
    }
}
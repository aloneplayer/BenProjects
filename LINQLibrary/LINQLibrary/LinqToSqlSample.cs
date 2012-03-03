using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQLibrary
{
    public class LinqToSqlSample
    {
        DCStudentDataContext  dc = new DCStudentDataContext();

        public void GetAllStudentsScore()
        {
            var sel = from s in dc.Students
                      join score in dc.StudentScores
                      on s.Id equals score.StudentId
                      select new
                      {
                          name = s.Name,
                          lesson = score.LessonName,
                          score = score.Score
                      };
        }

        /// <summary>
        /// 左外连接查询
        /// </summary>
        public void GetAllStudentsScore2()
        {
            var sel = from s in dc.Students
                      join score in dc.StudentScores
                      on s.Id equals score.StudentId
                      into tmp from a in tmp.DefaultIfEmpty()
                      select new
                      {
                          name = s.Name,
                          lesson = a.LessonName,
                          score = a.Score
                      };
        }

    }
}

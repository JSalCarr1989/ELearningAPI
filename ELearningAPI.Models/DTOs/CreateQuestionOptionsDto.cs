using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningAPI.Models.DTOs
{
    public class CreateQuestionOptionsDto
    {
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
    }
}

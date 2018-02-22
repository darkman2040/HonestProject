using HonestProject.DataModels;

namespace HonestProject.Validators
{
    public class ProjectValidator : BasicValidator 
    {
        HonestProjectContext context;
        public ProjectValidator(HonestProjectContext context)
        {
            this.context = context;
        }
    }
}
using HonestProject.DataModels;

namespace HonestProject.Validators {

    public class BasicValidator : IBasicValidator
    {
        public bool ValidationErrorDetected {get; set;}
        public string ValidationErrorMessage {get; set;}

        public BasicValidator()
        {
            ValidationErrorDetected = false;
        }

        public void SetError(string error)
        {
            ValidationErrorDetected = true;
            ValidationErrorMessage = error;
        }
    }
}
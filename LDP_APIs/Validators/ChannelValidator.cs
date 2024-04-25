using FluentValidation;
using LDP.Common.Requests;

namespace LDP_APIs.Validators
{
    public class AddChannelValidator : AbstractValidator<AddChannelRequest>
    {
        public AddChannelValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.ChannelName).NotNull().NotEmpty().WithMessage("Please check!.. ChannelName  is null or empty");
            RuleFor(x => x.ChannelDescription).NotNull().NotEmpty().WithMessage("Please check!.. ChannelDescription  is null or empty");
           // RuleFor(x => x.ChannelTypeId).GreaterThan(0).WithMessage("Please check!.. ChannelTypeId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateChannelValidator : AbstractValidator<UpdateChannelRequest>
    {
        public UpdateChannelValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ChannelName).NotNull().NotEmpty().WithMessage("Please check!.. ChannelName  is null or empty");
            RuleFor(x => x.ChannelDescription).NotNull().NotEmpty().WithMessage("Please check!.. ChannelDescription  is null or empty");
            RuleFor(x => x.ChannelTypeId).GreaterThan(0).WithMessage("Please check!.. ChannelTypeId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
    }
    public class DeleteChannelValidator : AbstractValidator<DeleteChannelRequest>
    {
        public DeleteChannelValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
        }
    }

    public class AddChannelSubItemValidator : AbstractValidator<AddChannelSubItemRequest>
    {
        public AddChannelSubItemValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.ChannelSubItemName).NotNull().NotEmpty().WithMessage("Please check!.. ChannelSubItemName  is null or empty");
            RuleFor(x => x.ChannelSubItemDescription).NotNull().NotEmpty().WithMessage("Please check!.. ChannelSubItemDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateChannelSubItemValidator : AbstractValidator<UpdateChannelSubItemRequest>
    {
        public UpdateChannelSubItemValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.ChannelSubItemName).NotNull().NotEmpty().WithMessage("Please check!.. ChannelSubItemName  is null or empty");
            RuleFor(x => x.ChannelSubItemDescription).NotNull().NotEmpty().WithMessage("Please check!.. ChannelSubItemDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
    }

    public class DeleteChannelSubItemValidator : AbstractValidator<DeleteChannelSubItemRequest>
    {
        public DeleteChannelSubItemValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ChannelSubItemId).GreaterThan(0).WithMessage("Please check!.. ChannelSubItemId should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
        }
    }

    public class AddChannelQuestionValidator : AbstractValidator<AddChannelQuestionRequest>
    {
        public AddChannelQuestionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");

            RuleFor(x => x.QuestionDescription).NotNull().NotEmpty().WithMessage("Please check!.. QuestionDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class AddChannelAnswerValidator : AbstractValidator<AddChannelAnswerRequest>
    {
        public AddChannelAnswerValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AnswerDescription).NotNull().NotEmpty().WithMessage("Please check!.. AnswerDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.ChannelQuestionId).GreaterThan(0).WithMessage("Please check!.. ChannelQuestionId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. CreatedDate is null or empty");

        }
    }

    public class UpdateChannelQuestionValidator : AbstractValidator<UpdateChannelQuestionRequest>
    {
        public UpdateChannelQuestionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.QuestionId).GreaterThan(0).WithMessage("Please check!.. QuestionId should be greater than zero");
            RuleFor(x => x.QuestionDescription).NotNull().NotEmpty().WithMessage("Please check!.. QuestionDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }
    }

    public class UpdateChannelAnswerValidator : AbstractValidator<UpdateChannelAnswerRequest>
    {
        public UpdateChannelAnswerValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AnswerId).GreaterThan(0).WithMessage("Please check!.. AnswerId should be greater than zero");
            RuleFor(x => x.QuestionId).GreaterThan(0).WithMessage("Please check!.. QuestionId should be greater than zero");
            RuleFor(x => x.AnswerDescription).NotNull().NotEmpty().WithMessage("Please check!.. QuestionDescription  is null or empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.ModifiedUserId).GreaterThan(0).WithMessage("Please check!.. ModifiedUserId should be greater than zero");
            RuleFor(x => x.ModifiedDate).NotNull().NotEmpty().WithMessage("Please check!.. ModifiedDate is null or empty");

        }


    }

    public class DeleteChannelQuestionValidator : AbstractValidator<DeleteChannelQuestionRequest>
    {
        public DeleteChannelQuestionValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.QuestionId).GreaterThan(0).WithMessage("Please check!.. QuestionId should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
    }

    public class DeleteChannelAnswerValidator : AbstractValidator<DeleteChannelAnswerRequest>
    {
        public DeleteChannelAnswerValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.AnswerId).GreaterThan(0).WithMessage("Please check!.. AnswerId should be greater than zero");
            RuleFor(x => x.DeletedUserId).GreaterThan(0).WithMessage("Please check!.. DeletedUserId should be greater than zero");
            RuleFor(x => x.DeletedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");

        }
    }

    public class FileUploadValidator : AbstractValidator<FileUploadRequest>
    {
        private readonly string[] allowedExtensions = { ".docx", ".doc", ".pdf" };
        private const int MaxFileSizeInBytes = 10 * 1024 * 1024; // 10MB

        public FileUploadValidator()
        {

            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Please check!.. Request data is empty");
            RuleFor(x => x.ChannelId).GreaterThan(0).WithMessage("Please check!.. ChannelId should be greater than zero");
            RuleFor(x => x.CreatedUserId).GreaterThan(0).WithMessage("Please check!.. CreatedUserId should be greater than zero");
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty().WithMessage("Please check!.. DeletedDate is null or empty");
           // RuleFor(x => x.File.Length).GreaterThan(0).WithMessage("Please check!.. File is null or empty");
            RuleFor(x => x.OrgId).GreaterThan(0).WithMessage("Please check!.. OrgId should be greater than zero");
            RuleFor(x => x.File)
                 .NotNull().WithMessage("File is required.")
                 .Must(BeAValidFile).WithMessage("Invalid file type.")
                 .Must(BeWithinFileSizeLimit).WithMessage($"File size should be less than {MaxFileSizeInBytes} bytes / 10 MB.");


        }

        private bool BeAValidFile(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = System.IO.Path.GetExtension(file.FileName);
                return allowedExtensions.Contains(fileExtension.ToLower());
            }

            return true; // Allow null file
        }

        private bool BeWithinFileSizeLimit(IFormFile file)
        {
            if (file != null)
            {
                return file.Length <= MaxFileSizeInBytes;
            }

            return true; // Allow null file
        }

    }
}

using Application.DTO;
using FluentValidation;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Validator_DTO
{
    public class DoctorApplicationCreateDtoValidator : AbstractValidator<DoctorApplicationCreateDto>
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly string[] allowedExtensions = { ".pdf", ".doc", ".docx" };

        private readonly string[] allowedSpecializations = {
            "Cardiology", "Dermatology", "Neurology", "Pediatrics", "Orthopedics",
            "General Surgery", "Ophthalmology", "Psychiatry", "ENT", "Urology",
            "Nephrology", "Gastroenterology", "Endocrinology", "Hematology", "Oncology",
            "Pulmonology", "Rheumatology", "Infectious Disease", "Allergy and Immunology",
            "Plastic Surgery", "Vascular Surgery", "Thoracic Surgery", "Neurosurgery",
            "Obstetrics and Gynecology", "Family Medicine", "Emergency Medicine",
            "Anesthesiology", "Radiology", "Pathology", "Geriatrics", "Sports Medicine",
            "Occupational Medicine", "Public Health", "Critical Care", "Medical Genetics",
            "Nuclear Medicine", "Forensic Medicine", "Dentistry", "Oral and Maxillofacial Surgery"
        };

        public DoctorApplicationCreateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .Matches(@"^[\p{L}\s]{3,100}$").WithMessage("Invalid full name. Name should be bigger than 2 characters and only letters and spaces are allowed.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .Must(s => allowedSpecializations.Contains(s, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Specialization is not valid.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+[1-9]\d{7,14}$|^01[0-2,5]\d{8}$")
                .WithMessage("Invalid phone number format. Must be international (e.g. +1234567890) or valid Egyptian number.");

            RuleFor(x => x.CVFilePath)
                .NotEmpty().WithMessage("CV file path is required.")
                .Must(BeAValidFile).WithMessage("Invalid CV file. Only PDF/DOC/DOCX are allowed, max size 5MB.");
        }

        private bool BeAValidFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return false;

            var extension = Path.GetExtension(filePath)?.ToLower();
            if (!allowedExtensions.Contains(extension)) return false;

            if (!File.Exists(filePath)) return false;

            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length <= MaxFileSize;
        }
    }
}


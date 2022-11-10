export {
  minLengthValidator,
  maxLengthValidator,
  requiredInputMsg,
  maxValueValidator,
  minValueValidator,
};

type RequiredInputLength = { requiredLength: string };

type RequiredInputValue = { requiredValue: string };

function minLengthValidator(context: RequiredInputLength): string {
  return `Minimum length — ${context.requiredLength}`;
}

function maxLengthValidator(context: RequiredInputLength): string {
  return `Maximum length — ${context.requiredLength}`;
}

function minValueValidator(context: RequiredInputValue): string {
  return `Minimum value is ${context.requiredValue}`;
}

function maxValueValidator(context: RequiredInputValue): string {
  return `Maximum value is ${context.requiredValue}`;
}

const requiredInputMsg = 'Enter a value';

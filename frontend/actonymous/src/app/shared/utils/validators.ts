export { minLengthValidator, maxLengthValidator, requiredInputMsg };

type RequiredInputLength = { requiredLength: string };

function minLengthValidator(context: RequiredInputLength): string {
  return `Minimum length — ${context.requiredLength}`;
}

function maxLengthValidator(context: RequiredInputLength): string {
  return `Maximum length — ${context.requiredLength}`;
}

const requiredInputMsg = "Enter a value";
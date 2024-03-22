import type {FormError} from "#ui/types";

interface userAnswer {
    userAnswer?: string;
}

function validateUserAnswer(state: userAnswer): FormError[] {
    const errors = []
    if (!state.userAnswer) errors.push({ path: 'userAnswer', message: 'Required' })
    return errors
}

export default validateUserAnswer;

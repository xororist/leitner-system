import type {FormError} from "#ui/types";

interface UserAnswer {
    UserAnswer?: string;
}

function validateUserAnswer(state: userAnswer): FormError[] {
    const errors = []
    if (!state.userAnswer) errors.push({ path: 'userAnswer', message: 'Required' })
    return errors
}

export default validateUserAnswer;

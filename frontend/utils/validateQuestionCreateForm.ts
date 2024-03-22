import type {FormError} from "#ui/types";

interface QuestionState {
    question?: string;
    answer?: string;
    tag?: string;
}

function validateCreateQuestion(state: QuestionState): FormError[] {
    const errors: FormError[] = [];
    if (!state.question) errors.push({ path: 'question', message: 'Required' });
    if (!state.answer) errors.push({ path: 'answer', message: 'Required' });
    if (!state.tag) errors.push({ path: 'tag', message: 'Required' });
    return errors;
}

export default validateCreateQuestion;

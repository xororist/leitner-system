import axios from 'axios';

const API_URL = 'http://localhost:8080/cards';

export default {
    async fetchAll() {
        return await axios.get(API_URL);
    },
    async fetchAllTodayReview() {
        return await axios.get(API_URL + "/quizz");
    },
    async create(question: any) {
        return await axios.post(API_URL, question);
    },
    async update(question: any) {
        return await axios.patch(`${API_URL}`, question);
    },
    async delete(questionId: string) {
        await axios.delete(`${API_URL}/delete/${questionId}`);
    },
};

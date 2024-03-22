<script setup lang="ts">
import axios from "axios";
import validateCreateQuestion from "~/utils/validateQuestionCreateForm";
import validateUserAnswer from "~/utils/validateQuestionCreateForm";
import questionCardService from "~/services/questionCardService";

const isModalOpen = ref(false);
const isSlideOpen = ref(false);
const questions = ref([])

const createQuestionState = reactive({
  question: '',
  answer: '',
  tag: ''
});

const modalDisplayState = reactive({
  question: '',
  tag: '',
  id: ''
});

const answerState = reactive({
  userAnswer: ''
});

const openModalWithRowData = (row: any) => {
  isModalOpen.value = true;
  answerState.userAnswer = '';
  modalDisplayState.question = row.question;
  modalDisplayState.tag = row.tag;
  modalDisplayState.id = row.id;
};

const resetCreateQuestionState = () => {
  createQuestionState.question = '';
  createQuestionState.answer = '';
  createQuestionState.tag = '';
};

async function onSubmitCreateQuestion() {
  isSlideOpen.value = false;
  try {
    await questionCardService.create(createQuestionState)
    resetCreateQuestionState()
    await fetchQuestionsForTodayReview()
  } catch (error) {
    console.error('Error creating card:', error);
  }
}

async function onSubmitAnswer() {
  try {
    await axios.post(`http://localhost:8080/cards/answer/${modalDisplayState.id}`, null, {
      params: {
        answer: answerState.userAnswer
      }
    });
    console.log('Answer submitted successfully');
    await fetchQuestionsForTodayReview();
  } catch (error) {
    console.error('Error submitting answer:', error);
  } finally {
    isModalOpen.value = false; 
  }
}

const columns = [{
  key: 'question',
  label: 'Question'
}, {
  key: 'category',
  label: 'Category'
}, {
  key: 'tag',
  label: 'Tag'
}, {
  key: 'actions'
}]


const q = ref('')

const filteredRows = computed(() => {
  if (!q.value) {
    return questions.value
  }

  return questions.value.filter((question) => {
    return Object.values(question).some((value) => {
      return String(value).toLowerCase().includes(q.value.toLowerCase())
    })
  })
})

const fetchQuestionsForTodayReview = async () => {
  try {
    const response = await axios.get(`http://localhost:8080/cards/quizz`);
    questions.value = response.data;
  } catch (error) {
    console.error('Failed to fetch questions:', error);
  }
};

onMounted(fetchQuestionsForTodayReview)

</script>

<template>
  <div class="flex px-3 py-3.5 border-b border-gray-200 dark:border-gray-700">
    <p class="font-medium mr-2">Today's questions for review</p>
    <UInput v-model="q" placeholder="Filter questions..." />
    <UButton variant="soft" label="Add Question" @click="isSlideOpen = true" class="ml-2"/>
  </div>
  <USlideover v-model="isSlideOpen">
    <UCard class="flex flex-col flex-1" :ui="{ body: { base: 'flex-1' }, ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
      <template #header>
        <span class="font-medium">Add Question</span>
      </template>

      <UForm :validate="validateCreateQuestion" :state="createQuestionState" class="space-y-4" @submit="onSubmitCreateQuestion">
        <UFormGroup label="Question" name="question">
          <UInput v-model="createQuestionState.question" />
        </UFormGroup>
        <UFormGroup label="Answer" name="answer">
          <UInput v-model="createQuestionState.answer"/>
        </UFormGroup>
        <UFormGroup label="Tag" name="tag">
          <UInput v-model="createQuestionState.tag"/>
        </UFormGroup>

        <UButton type="submit">
          Add
        </UButton>
      </UForm>
    </UCard>
  </USlideover>
  <UTable :rows="filteredRows" :columns="columns">
    <template #actions-data="{ row }">
      <UButton variant="soft" label="Answer" @click="() => openModalWithRowData(row)" />
    </template>
  </UTable>
  <UModal v-model="isModalOpen">
    <UCard>
      <UForm :validate="validateUserAnswer" :state="answerState" class="space-y-4" @submit="onSubmitAnswer">
        <p class="font-medium">{{ modalDisplayState.question }}</p>
        <UBadge color="blue" variant="subtle">{{ modalDisplayState.tag }}</UBadge>
        <UDivider/>
        <UFormGroup label="Answer" name="userAnswer">
          <UInput v-model="answerState.userAnswer" />
        </UFormGroup>
        <UButton type="submit">
          Submit Answer
        </UButton>
      </UForm>
    </UCard>
  </UModal>
</template>


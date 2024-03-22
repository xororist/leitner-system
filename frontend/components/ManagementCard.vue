<script setup lang="ts">
import questionCardService from "~/services/questionCardService";
import validateCreateQuestion from "~/utils/validateQuestionCreateForm";

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

const openModalWithRowData = (row: any) => {
  isModalOpen.value = true;
  modalDisplayState.id = row.id; 
  createQuestionState.question = row.question;
  createQuestionState.answer = row.answer;
  createQuestionState.tag = row.tag;
};

const resetCreateQuestionState = () => {
  createQuestionState.question = '';
  createQuestionState.answer = '';
  createQuestionState.tag = '';
};

const columns = [{
  key: 'question',
  label: 'Question'
}, {
  key: 'category',
  label: 'Category'
}, {
  key: 'tag',
  label: 'Tag'
},{
  key: 'answer',
  label: 'Answer'
},
{
  key: 'isCompleted',
  label: 'Completed'
},
{
  key: 'nextReviewDate',
  label: 'Next Review'
},
{
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

async function fetchAllQuestions() {
  try {
    const response = await questionCardService.fetchAll()
    questions.value = response.data
  } catch (error) {
    console.error('Failed to fetch questions:', error);
  }
}

async function onSubmitCreateQuestion() {
  isSlideOpen.value = false;
  try {
    await questionCardService.create(createQuestionState);
    resetCreateQuestionState()
    await fetchAllQuestions()
  } catch (error) {
    console.error('Error creating card:', error);
  }
}

const onDeleteQuestion = async (questionId: string) => {
  if (!confirm('Confirm to delete this question?')) {
    return;
  }
  try {
    await questionCardService.delete(questionId);
    isModalOpen.value = false;
    await fetchAllQuestions()
  } catch (error) {
    console.error('Failed to delete question:', error);
  }
};

async function updateCard() {
  if (!modalDisplayState.id) {
    console.error('No card ID specified for update.');
    return;
  }

  const updateCardDto = {
    CardId: modalDisplayState.id,
    Question: createQuestionState.question,
    Answer: createQuestionState.answer,
    Tag: createQuestionState.tag
  };

  try {
    await questionCardService.update(updateCardDto)
    await fetchAllQuestions();
    isModalOpen.value = false;
  } catch (error) {
    console.error('Error updating card:', error);
  }
}

onMounted(fetchAllQuestions)

</script>

<template>
    <div class="flex px-3 py-3.5 border-b border-gray-200 dark:border-gray-700">
      <p class="font-medium mr-2">All your questions</p>
      <UInput v-model="q" placeholder="Filter questions..." />
      <UButton variant="soft" label="Add Question" @click="isSlideOpen = true" class="ml-2"/>
    </div>
    <USlideover v-model="isSlideOpen">
      <UCard class="flex flex-col flex-1" :ui="{ body: { base: 'flex-1' }, ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
        <template #header>
          <span>Add Question</span>
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
        <UButton color="blue" variant="soft" label="Edit" @click="() => openModalWithRowData(row)" />
        <UButton color="red" variant="soft" label="Delete" @click="() => onDeleteQuestion(row.id)" class="ml-2" />
      </template>
    </UTable>
    <UModal v-model="isModalOpen">
      <UCard>
        <UForm :validate="validateCreateQuestion" :state="createQuestionState" class="space-y-4" @submit="updateCard">
          <UFormGroup label="Question" name="question">
            <UInput v-model="createQuestionState.question" />
          </UFormGroup>
          <UFormGroup label="Answer" name="answer">
            <UInput v-model="createQuestionState.answer" />
          </UFormGroup>
          <UFormGroup label="Tag" name="tag">
            <UInput v-model="createQuestionState.tag" />
          </UFormGroup>
          <UButton type="submit">Update Question</UButton>
        </UForm>
      </UCard>
    </UModal>
</template>
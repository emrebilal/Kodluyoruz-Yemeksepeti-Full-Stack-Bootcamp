const form = document.getElementById("form");
const input = document.getElementById("input");
const todosUL = document.getElementById("todos");
const addBtn = document.getElementById("addBtn");

const todos = JSON.parse(localStorage.getItem("todos"));
//Add if there is data in local
if (todos) {
    todos.forEach((todo) => {
        addTodo(todo);
    });
}
//Item submitted from the form
form.addEventListener("submit", (e) => {
    e.preventDefault();

    addTodo();
});
addBtn.addEventListener("click", (e) => {
    e.preventDefault();

    addTodo();
});

//
function addTodo(todo) {
    let todoText = input.value;

    if (todo) {
        todoText = todo.text;
    }

    if (todoText) {
        const todoElement = document.createElement("li");
        const deleteBtn = document.createElement("button");

        if (todo && todo.completed) {
            todoElement.classList.add("completed");
        }

        todoElement.innerText = todoText;
        deleteBtn.innerText = "Delete";
        todoElement.appendChild(deleteBtn);

        //Left click to done
        todoElement.addEventListener("click", () => {
            todoElement.classList.toggle("completed");

            SaveLS();
        });

        //Click delete button
        deleteBtn.addEventListener("click", (e) => {
            e.preventDefault();

            todoElement.remove();

            SaveLS();
        });

        todosUL.appendChild(todoElement);

        input.value = "";

        SaveLS();
    }
}

//Save to Local Storage
function SaveLS() {
    const todosElement = document.querySelectorAll("li");

    const todos = [];

    todosElement.forEach((todoElement) => {
        todos.push({
            text: todoElement.innerText,
            completed: todoElement.classList.contains("completed"),
        });
    });

    localStorage.setItem("todos", JSON.stringify(todos));
}

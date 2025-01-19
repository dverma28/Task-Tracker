# Task-Tracker
Task tracker is a project used to track and manage your tasks

## Project URL
[Task Manager](https://roadmap.sh/projects/task-tracker)

### **Detailed Explanation**

#### **1. Main Project (`Task-Manager/`)**
- **Models**: Contains the `Task` class that defines the structure of a task (e.g., ID, description, status, timestamps).
- **Repositories**: Implements the data access layer for reading/writing tasks to the JSON file.
- **Services**: Encapsulates business logic for task operations, such as validation, updating timestamps, and filtering tasks.
- **CLI**: Manages user input parsing and maps commands to appropriate service methods.

#### **2. Test Project (`Task-Manager.Test/`)**
- Separate test project for unit tests.
- Organized into subdirectories to match the structure of the main project.
- Use a testing framework like **xUnit**.
- Include tests for edge cases, such as invalid IDs or empty JSON files.

#### **3. Other Files**
- **`tasks.json`**: A runtime-generated JSON file to store tasks persistently.
- **`README.md`**: Documentation explaining how to set up, run, and test the project.

---

### **Advantages of This Structure**
1. **Modularity**: Each component has a clear responsibility.
2. **Scalability**: Easy to add new features or extend functionality.
3. **Testability**: Organized test structure ensures every component can be tested independently.
4. **Maintainability**: Changes in one layer (e.g., repository) won’t affect others directly.

---
## **Features**
- Add, update, and delete tasks.
- Mark tasks as **in progress** or **done**.
- List tasks by status (**todo**, **in progress**, **done**, or all).
- Tasks are stored in a local JSON file.

---

## **Setup**

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd TaskTrackerCLI
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project TaskTrackerCLI
   ```

---

## **Usage**

### **Add a Task**
```bash
task-cli add "Buy groceries"
```
**Output:** `Task added successfully (ID: 1)`

### **Update a Task**
```bash
task-cli update 1 "Buy groceries and cook dinner"
```
**Output:** `Task updated successfully`

### **Delete a Task**
```bash
task-cli delete 1
```
**Output:** `Task deleted successfully`

### **Mark a Task as In Progress**
```bash
task-cli mark-in-progress 2
```
**Output:** `Task 2 marked as in progress`

### **Mark a Task as Done**
```bash
task-cli mark-done 2
```
**Output:** `Task 2 marked as done`

### **List All Tasks**
```bash
task-cli list
```

### **List Tasks by Status**
#### List Completed Tasks
```bash
task-cli list done
```
#### List Pending Tasks
```bash
task-cli list todo
```
#### List In Progress Tasks
```bash
task-cli list in-progress
```

---

## **Task Properties**
Each task includes:
- **ID**: Unique identifier.
- **Description**: Short description of the task.
- **Status**: (`todo`, `in-progress`, `done`).
- **CreatedAt**: Timestamp of creation.
- **UpdatedAt**: Timestamp of last update.

---

## **Development**

### **Run Unit Tests**
To ensure the application is functioning as expected:
```bash
dotnet test
```

---

## **Contributing**
1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Description of changes"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request.

---

## **License**
This project is licensed under the Apache 2.0 License.

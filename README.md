# Task-Tracker
Task tracker is a project used to track and manage your tasks

### **Detailed Explanation**

#### **1. Main Project (`src/TaskTrackerCLI/`)**
- **Models**: Contains the `Task` class that defines the structure of a task (e.g., ID, description, status, timestamps).
- **Repositories**: Implements the data access layer for reading/writing tasks to the JSON file.
- **Services**: Encapsulates business logic for task operations, such as validation, updating timestamps, and filtering tasks.
- **CLI**: Manages user input parsing and maps commands to appropriate service methods.
- **Utils**: Contains helper classes for reusable functionalities like JSON serialization or error handling.

#### **2. Test Project (`tests/TaskTrackerCLI.Tests/`)**
- Separate test project for unit tests.
- Organized into subdirectories to match the structure of the main project.
- Use a testing framework like **xUnit** or **NUnit**.
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

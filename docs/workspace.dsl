workspace {
    model {
        User = person "User" "Person"

        ToDoApp = softwareSystem "TODO App" "Allows users to track tasks." {
            AngularUI = container "Web Interface" "Allows the user to view and create tasks." "Angular Project" {
                
            }

            TaskMS = container "Task Microservice" "Handles CRUD operations for tasks." ".NET Project" {
                TaskController = component "Task Controller" "Handles HTTP(s) communication" ".NET Controller"
                TaskService = component "Task Service" "Business logic"  ".NET Service"
                TaskRepository = component "Task Repository" "Communicates with the database" ".NET Service"
            }

            UserMS = container "User Microservice" "Handles CRUD operations for users."  ".NET Project" {
                UserController = component "User Controller" "Handles HTTP(s) communication" ".NET Controller"
                UserService = component "User Service" "Business logic"  ".NET Service"
                UserRepository = component "User Repository" "Communicates with the database" ".NET Service"
            }

            TaskDatabase = container "Task Database" "Persists created tasks" "PostgreSQL" {
                tags "Database"
            }
            
            UserDatabase = container "User Database" "Persists created users" "PostgreSQL" {
                tags "Database"
            }
        }

        // Relationships
        User -> AngularUI "Creates and views tasks" "HTTPS"

        AngularUI -> TaskMS "GET/POST/PUT/DELETE tasks" "HTTPS"
        AngularUI -> UserMS "GET/POST/PUT/DELETE users" "HTTPS"

        TaskController -> TaskService "Uses"
        TaskService -> TaskRepository "Uses"
        TaskRepository -> TaskDatabase "Reads from" "JDBC

        UserController -> UserService "Uses"
        UserService -> UserRepository "Uses"
        UserRepository -> UserDatabase "Reads from" "JDBC
    }

    views {
        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #003459
                shape person
            }
            element "Software System" {
                background #ba1e25
            }
            element "App" {
                shape "MobileDeviceLandscape"
            }
            element "Database" {
                shape cylinder
                background #61C9A8
            }
            element "Container" {
                background #d9232b
            }
            element "Component" {
                background #E66C5A
            }
            element "WebBrowser" {
                shape WebBrowser
            }
            element "Queue" {
                shape pipe
                background #ffcc00
            }
        }
    }
}
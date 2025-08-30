export class MainDashboard {
    constructor() {
        this.initDragAndDrop();
    }

    initDragAndDrop() {
        const tasks = document.querySelectorAll('.kanban-task');
        const columns = document.querySelectorAll('.kanban-tasks');

        let draggedTask = null;

        tasks.forEach(task => {
            task.draggable = true;

            task.addEventListener('dragstart', (e) => {
                draggedTask = task;
                e.dataTransfer.effectAllowed = 'move';
                task.classList.add('dragging');
            });

            task.addEventListener('dragend', () => {
                draggedTask = null;
                task.classList.remove('dragging');
            });
        });

        columns.forEach(column => {
            column.addEventListener('dragover', (e) => {
                e.preventDefault();
                e.dataTransfer.dropEffect = 'move';
                column.classList.add('drag-over');
            });

            column.addEventListener('dragleave', () => {
                column.classList.remove('drag-over');
            });

            column.addEventListener('drop', () => {
                if (draggedTask) {
                    column.appendChild(draggedTask);
                    column.classList.remove('drag-over');
                }
            });
        });
    }
}

window.MainDashboard = MainDashboard;

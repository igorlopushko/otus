package tasks

// Task interface to define task to be run
type Task interface {
	Run(data []string) string
}

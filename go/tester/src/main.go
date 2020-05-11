package main

import (
	"log"
	"os"

	"./tasks"
	"./testrunner"
)

func init() {
	log.SetOutput(os.Stdout)
}

func main() {
	//fmt.Println("Hello, World!!")

	var stringLengthTask tasks.StringLengthTask
	var runner testrunner.TestRunner

	runner.Run(stringLengthTask, "../data/0.String")
}

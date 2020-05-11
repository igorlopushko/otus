package testrunner

import (
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"path/filepath"
	"runtime"
	"strings"

	"../tasks"
)

// TestRunner provides type to run test set
type TestRunner struct{}

// Run test
func (r TestRunner) Run(t tasks.Task, path string) {
	count := 0

	for {
		inPath := filepath.FromSlash(fmt.Sprintf("%s/test.%d.in", path, count))
		outPath := filepath.FromSlash(fmt.Sprintf("%s/test.%d.out", path, count))
		if _, err := os.Stat(inPath); err != nil {
			break
		}

		if _, err := os.Stat(outPath); err != nil {
			break
		}

		fmt.Printf("Test # %d - %t\n", count, execute(t, inPath, outPath))
		count++
	}
}

func execute(t tasks.Task, in string, out string) bool {
	dataArr, err := ioutil.ReadFile(in)
	if err != nil {
		log.Fatal(err)
	}

	expectedArr, err := ioutil.ReadFile(out)
	if err != nil {
		log.Fatal(err)
	}

	var data, expected string

	if runtime.GOOS == "windows" {
		data = strings.TrimRight(string(dataArr), "\r\n")
		expected = strings.TrimRight(string(expectedArr), "\r\n")
	} else {
		data = strings.TrimRight(string(dataArr), "\n")
		expected = strings.TrimRight(string(expectedArr), "\n")
	}

	actual := t.Run([]string{string(data)})

	return actual == expected
}

package tasks

import (
	"strconv"
	"unicode/utf8"
)

// StringLengthTask provides type for calculating string length
type StringLengthTask struct{}

func (t StringLengthTask) Run(data []string) string {
	return strconv.Itoa(utf8.RuneCountInString(data[0]))
}

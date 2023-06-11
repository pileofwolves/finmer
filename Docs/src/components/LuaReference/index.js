/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

import React from 'react';
import styles from './styles.module.css';

// Borrowed from Admonition
function NoteIcon() {
  return (
    <svg viewBox="0 0 14 16">
      <path
        fillRule="evenodd"
        d="M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"
      />
    </svg>
  );
}

// Returns HTML elements describing the function signature
function MakeArgumentList(entry) {
	// We still want to draw the empty argument list '()' even if there are no arguments
	const arg_list = entry.input ?? [];
	return (
		<div className={styles.signature}>
			{arg_list.map((argument, index) =>
				<div className={argument.optional && styles.optional}>{argument.name}</div>
			)}
		</div>
	)
}

// Returns HTML elements describing the input parameter list
function MakeInputList(entry) {
	if (!entry.input || entry.input.length == 0)
		return null;

	return (
		<ul className={styles.input}>
		{
			entry.input.map((param, index) =>
				<li>
					<em>{param.name}</em> ({param.type}) - {param.optional && "optional -"} {param.desc}
				</li>
			)
		}
		</ul>
	)
}

// Returns HTML elements describing the return value list
function MakeOutputList(entry) {
	if (!entry.output || entry.output.length == 0)
		return null;

	return (
		<ul className={styles.output}>
		{
			entry.output.map((param, index) =>
				<li>
					({param.type}) - {param.desc}
				</li>
			)
		}
		</ul>
	)
}

// Returns HTML elements describing the origin of the function
function MakeOriginNote(entry) {
	if (!entry.coremod)
		return null;

	const text_main = "This function is part of the Core module. (Hover for details)";
	const text_tooltip = "This function is not built-in to the game engine, but rather defined by the official Core module. If your mod setup does not load the Core module, such as with a 'total conversion' style mod, this function is not available.";
	return (
		<div className={styles.origin_coremod} title={text_tooltip}>
			{NoteIcon()}
			{text_main}
		</div>
	)
}

// Generates a function reference document block
function MakeFunctionReference(entry) {
	return (
		<div className={styles.scriptapi}>
			<div className={styles.title}>
				<div className={styles.qualifier}>{entry.qualifier}</div>
				{entry.name}
				{MakeArgumentList(entry)}
			</div>
			{MakeInputList(entry)}
			{MakeOutputList(entry)}
			<div className={styles.desc}>{entry.remarks}</div>
			{MakeOriginNote(entry)}
		</div>
	)
}

// Generates a property reference document block
function MakePropertyReference(entry) {
	return (
		<div className={styles.scriptapi}>
			<div className={styles.title}>
				<div className={styles.qualifier}>{entry.qualifier}</div>
				{entry.name}
			</div>
			<ul className={styles.type}>
				<li>{entry.valuetype} - {entry.writable ? "read/write" : "read-only"}</li>
			</ul>
			<div className={styles.desc}>{entry.remarks}</div>
		</div>
	)
}

// Generates documentation for an API element
function LuaReferenceBlock(entry) {
	return (
		<>
			<a id="toc1" />
			{entry.type == "function" ? MakeFunctionReference(entry) : MakePropertyReference(entry)}
		</>
	)
}

// Generates a LuaReferenceBlock for each API element in an API group
export default function LuaReferenceList(props) {
	return (
		<div>
		{
			props.group.map((entry, index) =>
				<React.Fragment>
					{LuaReferenceBlock(entry)}
				</React.Fragment>
			)
		}
		</div>
	)
}

/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

import React from 'react';
import styles from './styles.module.css';

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
